namespace BugSharp.Remote
{
    internal enum Endpoints
    {
        Bug,
        BugSearch,
        Comment,
        Attachment,
        Component,
        Field
    }
    
    internal static class EndpointsExtension 
    {
        public static string ToUri(this Endpoints endpoint, int id = -1)
        {
            switch (endpoint)
            {
                case Endpoints.Bug:
                    return "bug/" + (id != -1 ? id.ToString() : "");
                case Endpoints.Comment:
                    return "bug/" + id + "/comment";
                case Endpoints.Attachment:
                    return "bug/attachment/" + id;
                case Endpoints.Component:
                    return "component";
                case Endpoints.Field:
                    return "field/bug/" + (id != -1 ? id.ToString() : "");
                default:
                    return string.Empty;
            }
        }
        
        public static string ToUri(this Endpoints endpoint, string urlParams)
        {
            switch (endpoint)
            {
                case Endpoints.Field:
                    return "field/bug/" + urlParams;
                default:
                case Endpoints.BugSearch:
                    return "bug?" + urlParams;
            }
        }
    }
}