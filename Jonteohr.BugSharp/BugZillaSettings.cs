namespace BugSharp
{
    public class BugZillaSettings
    {
        public BugZillaSettings(string url, string apiKey)
        {
            BugZillaUrl = url;
            ApiKey = apiKey;
        }
        
        /// <summary>
        /// The base URL to the BugZilla instance
        /// </summary>
        public string BugZillaUrl { get; }
        
        /// <summary>
        /// The API key to use when calling the REST API
        /// </summary>
        public string ApiKey { get; }
    }
}