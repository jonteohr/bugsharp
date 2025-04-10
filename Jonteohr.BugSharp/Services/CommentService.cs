using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp.Services
{
    internal class CommentService : BaseRequestClient, ICommentsService
    {
        private readonly BugZilla _bugZilla;
        
        public CommentService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }

        public async Task<List<Comment>> GetCommentsAsync(int bugId)
        {
            var jsonResult = await GetAsync(Endpoints.Comment, bugId, _bugZilla.Settings.ApiKey);
            var response = JsonConvert.DeserializeObject<RootObject>(jsonResult);

            if (response.Bugs.TryGetValue(bugId, out var comments))
            {
                return comments.Comments.Select(comment => new Comment(_bugZilla, comment)).ToList();
            }
            
            throw new BugNotFoundException($"Bug {bugId} not found or contains no comments");
        }

        public Task<int> CreateCommentAsync(Comment comment)
        {
            throw new RowNotInTableException();
        }
    }
}