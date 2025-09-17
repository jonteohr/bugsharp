using System.Collections.Generic;

namespace BugSharp.Remote
{
    public struct ProductResponse
    {
        public List<RemoteProduct> Products { get; set; }
    }
}