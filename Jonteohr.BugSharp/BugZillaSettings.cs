namespace BugSharp
{
    /// <summary>
    /// Settings for bugzilla client
    /// </summary>
    public class BugZillaSettings
    {
        /// <summary>
        /// Create new bugzilla settings
        /// </summary>
        /// <param name="url">The url to the remote bugzilla server</param>
        /// <param name="apiKey">The API key to use for calls</param>
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