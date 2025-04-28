using System;

namespace BugSharp.Exceptions
{
    /// <summary>
    /// The generic exception thrown whenever a call failed due to some reason explained in the message
    /// </summary>
    public class BugZillaRequestException : BugZillaException
    {
        /// <summary>
        /// Create a new exception
        /// </summary>
        public BugZillaRequestException(string message = null, Exception innerException = null) : base(message, innerException)
        {
        }
    }
}