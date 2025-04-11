using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp.Services
{
    internal class BugService : BaseRequestClient, IBugService
    {
        private readonly BugZilla _bugZilla;
        
        public BugService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }
        
        public async Task<Bug> GetBugAsync(int bugId)
        {
            var jsonResult = await GetAsync(Endpoints.Bug, bugId, _bugZilla.Settings.ApiKey);
            var response = JsonConvert.DeserializeObject<BugResponse>(jsonResult);

            if (response.Bugs.Count < 1)
                throw new BugNotFoundException($"Bug {bugId} not found.");
            
            var bug = new Bug(_bugZilla, response.Bugs.FirstOrDefault());
            
            return bug;
        }

        public async Task<List<Bug>> GetBugsAsync(int[] ids)
        {
            var search = _bugZilla.CreateSearch();
            search.Id = ids;

            return await search.SearchBugsAsync();
        }

        public async Task UpdateBugAsync(Bug bug)
        {
            var json = JsonConvert.SerializeObject(bug);
            await PutAsync(Endpoints.Bug, bug.Id, json, _bugZilla.Settings.ApiKey);
        }

        public async Task<int> CreateBugAsync(Bug bug)
        {
            var json = JsonConvert.SerializeObject(bug);
            var response = await PostAsync(Endpoints.Bug, json, _bugZilla.Settings.ApiKey);

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

        public async Task<List<Bug>> SearchBugsAsync(BugSearch searchQuery)
        {
            var param = searchQuery.ToQueryString();
            var jsonResponse = await GetAsync(Endpoints.BugSearch, param, _bugZilla.Settings.ApiKey);
            var response = JsonConvert.DeserializeObject<BugResponse>(jsonResponse);

            if (response.Bugs == null || response.Bugs.Count < 1)
                return new List<Bug>();
            
            return response.Bugs.Select(bug => new Bug(_bugZilla, bug)).ToList();
        }
    }
}