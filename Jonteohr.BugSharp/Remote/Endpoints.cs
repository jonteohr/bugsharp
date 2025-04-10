namespace BugSharp.Remote
{
    internal enum Endpoints
    {
        Bug,
        Comment,
        Attachment
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
                default:
                    return string.Empty;
            }
        }
    }
}