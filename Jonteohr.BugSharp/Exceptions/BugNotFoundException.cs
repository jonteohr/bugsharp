using System;

namespace BugSharp.Exceptions
{
    /// <summary>
    /// The exception thrown for when a requested bug isn't found on the remote server
    /// </summary>
    public class BugNotFoundException : Exception
    {
        /// <summary>
        /// Create a new exception
        /// </summary>
        public BugNotFoundException(string message = null, Exception inner = null) : base(message, inner)
        {}
    }
}