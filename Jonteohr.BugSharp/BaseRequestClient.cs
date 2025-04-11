using System.Net;
using System.Threading.Tasks;
using BugSharp.Remote;
using RestSharp;

namespace BugSharp
{
    internal abstract class BaseRequestClient
    {
        private readonly BugZillaSettings _settings;
        
        internal BaseRequestClient(BugZillaSettings settings)
        {
            _settings = settings;
        }
        
        internal async Task<string> GetAsync(Endpoints endPoint, string apiKey = null)
        {
            var client = new RestClient(_settings.BugZillaUrl);
            var req = new RestRequest("/rest/" + endPoint.ToUri(), Method.Get);
            if(!string.IsNullOrEmpty(apiKey))
                req.AddQueryParameter("api_key", apiKey);
        
            var response = await client.ExecuteAsync(req);
            return response.Content;
        }
        
        internal async Task<string> GetAsync(Endpoints endPoint, int id, string apiKey = null)
        {
            var client = new RestClient(_settings.BugZillaUrl);
            var req = new RestRequest("/rest/" + endPoint.ToUri(id), Method.Get);
            if(!string.IsNullOrEmpty(apiKey))
                req.AddQueryParameter("api_key", apiKey);
        
            var response = await client.ExecuteAsync(req);
            return response.Content;
        }
        
        internal async Task<string> GetAsync(Endpoints endPoint, string urlParams, string apiKey = null)
        {
            var client = new RestClient(_settings.BugZillaUrl);
            var req = new RestRequest("/rest/" + endPoint.ToUri(urlParams), Method.Get);
            if(!string.IsNullOrEmpty(apiKey))
                req.AddQueryParameter("api_key", apiKey);
        
            var response = await client.ExecuteAsync(req);
            return response.Content;
        }

        internal async Task<bool> PutAsync(Endpoints endpoint, int id, string json, string apiKey = null)
        {
            var client = new RestClient(_settings.BugZillaUrl);
            var req = new RestRequest("/rest/" + endpoint.ToUri(id), Method.Put);
            if(!string.IsNullOrEmpty(apiKey))
                req.AddQueryParameter("api_key", apiKey);

            req.AddJsonBody(json);
            var response = await client.ExecuteAsync(req);
            return response.StatusCode == HttpStatusCode.OK;
        }

        internal async Task<string> PostAsync(Endpoints endpoint, string json, string apiKey = null)
        {
            var client = new RestClient(_settings.BugZillaUrl);
            var req = new RestRequest("/rest/" + endpoint.ToUri(), Method.Post);
            if(!string.IsNullOrEmpty(apiKey))
                req.AddQueryParameter("api_key", apiKey);
            
            req.AddJsonBody(json);

            var response = await client.ExecuteAsync(req);
            return response.Content;
        }
    }
}