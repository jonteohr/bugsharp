using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugSharp
{
    /// <summary>
    /// The bugzilla information service
    /// </summary>
    public interface IBugzillaInformation
    {
        /// <summary>
        /// Returns the current version of Bugzilla. Normally in the format of X.X or X.X.X. For example, 4.4 for the initial release of a new branch. Or 4.4.6 for a minor release on the same branch.
        /// </summary>
        /// <returns>The current version of this Bugzilla</returns>
        Task<string> GetVersion();

        /// <summary>
        /// Returns the timezone in which Bugzilla expects to receive dates and times on the API. Currently hard-coded to UTC (“+0000”). This is unlikely to change.
        /// </summary>
        /// <returns>The timezone offset as a string in (+/-)XXXX (RFC 2822) format.</returns>
        Task<string> GetTimezone();
        
        /// <summary>
        /// Gets information about the extensions that are currently installed and enabled in this Bugzilla.
        /// </summary>
        /// <returns>
        /// An object containing the extensions enabled as keys. Each extension object contains the following keys:
        /// <list type="bullet">
        /// <item>version (string) The version of the extension</item>
        /// </list>
        /// </returns>
        Task<List<Extension>> GetExtensions();
        
        /// <summary>
        /// Gets information about what time the Bugzilla server thinks it is, and what timezone it’s running in.
        /// </summary>
        /// <returns>A <see cref="Time"/> object</returns>
        Task<Time> GetTime();
    }
}