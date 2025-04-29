using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Exceptions;

namespace BugSharp
{
    /// <summary>
    /// The bugzilla users service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Logging in with a username and password is required for many Bugzilla installations, in order to search for private bugs, post new bugs, etc. This method allows you to retrieve a token that can be used as authentication for subsequent API calls. Otherwise yuou will need to pass your login and password with each call.
        /// <br /><br />
        /// This method will be going away in the future in favor of using API keys.
        /// </summary>
        /// <param name="username">The user’s login name.</param>
        /// <param name="password">The user’s password.</param>
        /// <returns>A <see cref="User"/> instance</returns>
        /// <exception cref="BugZillaRequestException">If invalid login or other error occured.</exception>
        /// <seealso cref="Validate"/>
        /// <seealso cref="Logout"/>
        Task<User> Login(string username, string password);

        Task Logout(string token);

        Task<bool> Validate(string username, string token);

        Task<List<User>> GetUser();
    }
}