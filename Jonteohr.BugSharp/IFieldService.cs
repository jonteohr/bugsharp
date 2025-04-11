using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugSharp
{
    /// <summary>
    /// The bug field handling service
    /// </summary>
    public interface IFieldService
    {
        /// <summary>
        /// Gets a single bug field
        /// </summary>
        /// <param name="fieldId">The field ID</param>
        /// <returns>A <see cref="Field"/> populated with data</returns>
        /// <seealso cref="GetFieldAsync(string)"/>
        Task<Field> GetFieldAsync(int fieldId);
        
        /// <summary>
        /// Gets a single bug field
        /// </summary>
        /// <param name="fieldName">The field name</param>
        /// <returns>A <see cref="Field"/> populated with data</returns>
        /// <seealso cref="GetFieldAsync(int)"/>
        Task<Field> GetFieldAsync(string fieldName);
        
        /// <summary>
        /// Gets all fields available on the server
        /// </summary>
        /// <returns>A <see cref="List{Field}"/> of <see cref="Field"/></returns>
        Task<List<Field>> GetAllFieldsAsync();
    }
}