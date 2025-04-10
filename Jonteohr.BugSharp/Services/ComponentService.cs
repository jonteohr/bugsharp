using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp.Services
{
    internal class ComponentService : BaseRequestClient, IComponentService
    {
        private readonly BugZilla _bugZilla;
        
        public ComponentService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }

        public async Task<int> CreateComponentAsync(Component component)
        {
            var json = JsonConvert.SerializeObject(component);
            var response = await PostAsync(Endpoints.Component, json, _bugZilla.Settings.ApiKey);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            if (dict.TryGetValue("id", out var idObj))
            {
                if (int.TryParse(idObj.ToString(), out var id))
                {
                    return id;
                }
            }

            return -1;
        }
    }
}