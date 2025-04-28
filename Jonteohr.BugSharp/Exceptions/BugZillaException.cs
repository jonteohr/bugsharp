using System;

namespace BugSharp.Exceptions
{
    /// <summary>
    /// The generic exception for whenever some configuration of the client is invalid.
    /// </summary>
    public class BugZillaException : Exception
    {
        /// <summary>
        /// Create a new exception
        /// </summary>
        public BugZillaException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}