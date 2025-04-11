namespace BugSharp.Remote
{
    internal enum Endpoints
    {
        Bug,
        BugSearch,
        Comment,
        Attachment,
        Component
    }
    
    internal static class EndpointsExtension 
    {
        public static string ToUri(this Endpoints endpoint, int bugId = -1)
        {
            switch (endpoint)
            {
                case Endpoints.Bug:
                    return "bug/" + (bugId != -1 ? bugId.ToString() : "");
                case Endpoints.Comment:
                    return "bug/" + bugId + "/comment";
                case Endpoints.Attachment:
                    return "bug/attachment/" + bugId;
                case Endpoints.Component:
                    return "component";
                default:
                    return string.Empty;
            }
        }
        
        public static string ToUri(this Endpoints endpoint, string urlParams)
        {
            switch (endpoint)
            {
                default:
                case Endpoints.BugSearch:
                    return "bug?" + urlParams;
            }
        }
    }
}