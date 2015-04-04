
namespace Taut.Authorizations
{
    /// <summary>
    /// Manage the authorized user. Only one user can be authorized at a time.
    /// </summary>
    public interface IUserCredentialService
    {
        /// <summary>
        /// Is there an authorized user?
        /// </summary>
        bool IsAuthorized { get; }

        /// <summary>
        /// Get the authorized user.
        /// </summary>
        /// <returns>Null if no user is authorized. Otherwise, the authorized user.</returns>
        Authorization Authorization { get; set; }
    }
}
