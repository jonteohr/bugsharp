using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Exceptions;

namespace BugSharp
{
    /// <summary>
    /// The service for working with comments
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Get all comments connected to a bug
        /// </summary>
        /// <param name="bugId">The ID of the bug</param>
        /// <returns>A List filled with Comment</returns>
        /// <exception cref="BugNotFoundException">Bug was not found on the server.</exception>
        Task<List<Comment>> GetCommentsAsync(int bugId);
        
        /// <summary>
        /// Create a new comment on the BugZilla remote server
        /// </summary>
        /// <param name="comment">The comment object to create.</param>
        /// <returns>The comment ID if successful, otherwise -1</returns>
        Task<int> CreateCommentAsync(Comment comment);
    }
}