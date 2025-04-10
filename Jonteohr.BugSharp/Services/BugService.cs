using System;
using System.Threading.Tasks;

namespace BugSharp.Services
{
    public class BugService : BaseRequestClient, IBugService
    {
        private readonly BugZilla _bugZilla;
        
        public BugService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }

        public async Task<Bug> GetBugAsync(int bugId)
        {
            var jsonResult = await GetAsync("bug/" + bugId, _bugZilla.Settings.ApiKey);

            Console.WriteLine(jsonResult);
            return new Bug();
        }
    }
}