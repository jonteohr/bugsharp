using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Exceptions;

namespace BugSharp
{
    /// <summary>
    /// The bug handling service
    /// </summary>
    public interface IBugService
    {
        /// <summary>
        /// Get a bug from the BugZilla server
        /// </summary>
        /// <param name="bugId">The ID of the requested bug</param>
        /// <exception cref="BugNotFoundException">Bug was not found on the server.</exception>
        Task<Bug> GetBugAsync(int bugId);
        
        /// <summary>
        /// Gets several bugs matching the IDs specified
        /// </summary>
        /// <param name="ids">An array of integers with bug IDs</param>
        /// <returns>A <see cref="List{T}"/> of <see cref="Bug"/></returns>
        Task<List<Bug>> GetBugsAsync(int[] ids);
        
        /// <summary>
        /// Update a bug and save it on the remove server
        /// </summary>
        /// <param name="bug">The modified <see cref="Bug"/> object</param>
        Task UpdateBugAsync(Bug bug);
        
        /// <summary>
        /// Create a new bug on the BugZilla remote server
        /// </summary>
        /// <param name="bug">The <see cref="Bug"/> object to create.</param>
        /// <returns>The Bug ID if successful, otherwise -1</returns>
        Task<int> CreateBugAsync(Bug bug);

        /// <summary>
        /// Search for bugs matching a query
        /// </summary>
        /// <param name="searchQuery">The search query</param>
        /// <returns><see cref="List{T}"/> of <see cref="Bug"/></returns>
        Task<List<Bug>> SearchBugsAsync(BugSearch searchQuery);
    }
}