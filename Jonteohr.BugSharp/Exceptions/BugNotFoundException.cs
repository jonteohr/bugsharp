using System;

namespace BugSharp.Exceptions
{
    public class BugNotFoundException : Exception
    {
        public BugNotFoundException(string message, Exception inner) : base(message, inner)
        {}
    }
}