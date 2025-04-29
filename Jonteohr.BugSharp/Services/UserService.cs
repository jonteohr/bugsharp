using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp.Services
{
    internal class UserService : BaseRequestClient, IUserService
    {
        private readonly BugZilla _bugZilla;
        
        public UserService(BugZilla bugzilla) : base(bugzilla.Settings)
        {
            _bugZilla = bugzilla;
        }

        public async Task<User> Login(string username, string password)
        {
            var parameters = $"login={username}&password={password}";
            var json = await GetAsync(Endpoints.Login, parameters, _bugZilla.Settings.ApiKey);
            Console.WriteLine(json);
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            if (!response.TryGetValue("token", out var token) || !response.TryGetValue("id", out var id))
                throw new BugZillaRequestException("Could not login on the remote server. The credentials may be incorrect!");
            
            var returnUser = new User(int.Parse(id.ToString()), username, token.ToString());
            return returnUser;
        }

        public Task Logout(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Validate(string username, string token)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetUser()
        {
            throw new NotImplementedException();
        }
    }
}