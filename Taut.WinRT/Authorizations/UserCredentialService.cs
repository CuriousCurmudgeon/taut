using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;
using Windows.Security.Credentials;

namespace Taut.WinRT.Authorizations
{
    /// <summary>
    /// Store Authorization data in the Password Vault. Only one user can be authorized
    /// at a time.
    /// </summary>
    /// <remarks>
    /// The Password Vault just wants a string for the password. Since we have Authorizations
    /// instead of passwords, we store a serialized version of that instead.
    /// </remarks>
    public class UserCredentialService : IUserCredentialService
    {
        private const string RESOURCE_NAME = "Taut";
        // We don't actually care what the user name is, but we have to supply some value.
        private const string USERNAME = "default_slack_user";

        private PasswordVault _vault;

        public UserCredentialService()
        {
            _vault = new PasswordVault();
        }

        #region IUserCredentialService

        public bool IsAuthorized
        {
            get
            {
                return _vault.RetrieveAll().Count > 0;
            }
        }

        public Authorization Authorization
        {
            get { return GetAuthorization(); }
            set { SetAuthorization(value); }
        }

        #endregion

        #region Private Helpers

        private Authorization GetAuthorization()
        {
            if (_vault.RetrieveAll().Count == 0)
            {
                return null;
            }
            else
            {
                var credential = _vault.Retrieve(RESOURCE_NAME, USERNAME);
                var authorizationParts = credential.Password.Split('|');
                return new Authorization
                {
                    AccessToken = authorizationParts[0],
                    Scope = authorizationParts[1],
                };
            }
        }

        private void SetAuthorization(Authorization authorization)
        {
            ClearAuthorization();

            if (authorization != null)
            {
                var authorizationAsPassword = string.Format("{0}|{1}", authorization.AccessToken, authorization.Scope);
                _vault.Add(new PasswordCredential(RESOURCE_NAME, USERNAME, authorizationAsPassword));
            }
        }

        private void ClearAuthorization()
        {
            // In practice, multiple authorizations should never end up in the Password Vault, but
            // we'll assume that it is possible here.
            foreach (var credential in GetAllPasswordCredentials())
            {
                _vault.Remove(credential);
            }
        }

        private IReadOnlyList<PasswordCredential> GetAllPasswordCredentials()
        {
            return _vault.RetrieveAll();
        }

        #endregion
    }
}
