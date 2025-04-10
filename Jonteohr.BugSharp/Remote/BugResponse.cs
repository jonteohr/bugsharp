using System.Collections.Generic;

namespace BugSharp.Remote
{
    /// <summary>
    /// The first response of a bug
    /// </summary>
    public struct BugResponse
    {
        /// <summary>
        /// A list of bugs from a response
        /// </summary>
        public List<RemoteBug> Bugs { get; set; }
    }
}