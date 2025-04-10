namespace BugSharp
{
    /// <summary>
    /// The builder class to create a configured bugzilla client
    /// </summary>
    public class BugZillaClientBuilder
    {
        private BugZilla client;
        private string url;
        private string apiKey;
        
        /// <summary>
        /// Build the BugZilla client with specified settings
        /// </summary>
        /// <returns>A BugZilla client instance</returns>
        public BugZilla Build()
        {
            var settings = new BugZillaSettings(apiKey, url);
            var services = new ServiceLocator();
            client = new BugZilla(services, settings);
            BugZilla.ConfigureDefaultServices(services, client);
            return client;
        }

        /// <summary>
        /// Sets the base URL to the bugzilla server instance
        /// </summary>
        /// <param name="url">The URL to bugzilla home page</param>
        /// <example>SetUrl("https://my.website/bugzilla/")</example>
        public BugZillaClientBuilder SetUrl(string url)
        {
            this.url = url;
            return this;
        }

        /// <summary>
        /// Sets a generated API key to use for calls to the REST API
        /// </summary>
        /// <param name="key">A genereted API key</param>
        public BugZillaClientBuilder SetApiKey(string key)
        {
            this.apiKey = key;
            return this;
        }
    }
}