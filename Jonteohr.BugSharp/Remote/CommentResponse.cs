using System.Collections.Generic;

namespace BugSharp.Remote
{
    /// <summary>
    /// The root object of a request
    /// </summary>
    public struct RootObject
    {
        /// <summary>
        /// A list of bugs
        /// </summary>
        public Dictionary<int, BugComments> Bugs { get; set; }
    }
    
    /// <summary>
    /// The object of comments assigned to a bug
    /// </summary>
    public struct BugComments
    {
        /// <summary>
        /// A list of comments
        /// </summary>
        public List<RemoteComment> Comments { get; set; }
    }
}