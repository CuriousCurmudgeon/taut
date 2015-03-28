
namespace Taut.Authorization
{
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
        Authorization GetAuthorization();

        /// <summary>
        /// Add an authorized user.
        /// </summary>
        /// <param name="authorization">The authorization to add.</param>
        void AddAuthorization(Authorization authorization);

        /// <summary>
        /// Clear the authorized user.
        /// </summary>
        void ClearAuthorization();
    }
}
