using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp.Services
{
    internal class FieldService : BaseRequestClient, IFieldService
    {
        private readonly BugZilla _bugZilla;
        
        public FieldService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }

        public async Task<Field> GetFieldAsync(int fieldId)
        {
            var response = await GetAsync(Endpoints.Field, fieldId, _bugZilla.Settings.ApiKey);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<Field>>>(response);
            
            if ((!dict.TryGetValue("fields", out var fields)) || fields.Count < 1)
                throw new BugZillaRequestException();
            
            return fields.FirstOrDefault();
        }

        public async Task<Field> GetFieldAsync(string fieldName)
        {
            var response = await GetAsync(Endpoints.Field, fieldName, _bugZilla.Settings.ApiKey);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<Field>>>(response);
            
            if ((!dict.TryGetValue("fields", out var fields)) || fields.Count < 1)
                throw new BugZillaRequestException();
            
            return fields.FirstOrDefault();
        }

        public async Task<List<Field>> GetAllFieldsAsync()
        {
            var response = await GetAsync(Endpoints.Field, -1, _bugZilla.Settings.ApiKey);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<Field>>>(response);

            if (!dict.TryGetValue("fields", out var fields))
                throw new BugZillaRequestException();
            
            return fields;
        }
    }
}