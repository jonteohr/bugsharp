using System.Threading.Tasks;
using RestSharp;

namespace BugSharp
{
    public abstract class BaseRequestClient
    {
        private readonly BugZillaSettings _settings;
        
        internal BaseRequestClient(BugZillaSettings settings)
        {
            _settings = settings;
        }
        
        internal async Task<string> GetAsync(string endPoint, string apiKey = null)
        {
            var client = new RestClient(_settings.BugZillaUrl);
            var req = new RestRequest("/rest/" + endPoint, Method.Get);
            if(!string.IsNullOrEmpty(apiKey))
                req.AddQueryParameter("api_key", apiKey);
        
            var response = await client.ExecuteAsync(req);
            return response.Content;
        }
    }
}