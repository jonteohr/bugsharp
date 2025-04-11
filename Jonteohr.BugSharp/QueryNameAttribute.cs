using System;

namespace BugSharp
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class QueryNameAttribute : Attribute
    {
        public string Name { get; }

        public QueryNameAttribute(string name)
        {
            Name = name;
        }
    }
}