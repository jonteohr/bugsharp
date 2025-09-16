using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Remote;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BugSharp.Services
{
    internal class BugzillaService : BaseRequestClient, IBugzillaInformation
    {
        private readonly BugZilla _bugZilla;
        public BugzillaService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }


        public async Task<string> GetVersion()
        {
            var response = await GetAsync(Endpoints.Version, _bugZilla.Settings.ApiKey);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            return dict["version"];
        }

        public async Task<string> GetTimezone()
        {
            var response = await GetAsync(Endpoints.Timezone, _bugZilla.Settings.ApiKey);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            return dict["timezone"];
        }

        public async Task<List<Extension>> GetExtensions()
        {
            var response = await GetAsync(Endpoints.Extensions, _bugZilla.Settings.ApiKey);
            var jObject = JObject.Parse(response);
            var extensions = new List<Extension>();

            var extensionToken = jObject["extensions"] as JObject;
            if(extensionToken == null)
                return extensions;

            foreach (var prop in extensionToken.Properties())
            {
                extensions.Add(new Extension {Name = prop.Name, Version = prop.Value["version"]?.ToString()});
            }
            
            return extensions;
        }

        public async Task<Time> GetTime()
        {
            var response = await GetAsync(Endpoints.Time, _bugZilla.Settings.ApiKey);
            var remote = JsonConvert.DeserializeObject<RemoteTime>(response);

            return new Time(remote);
        }
    }
}