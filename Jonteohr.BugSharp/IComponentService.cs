using System.Threading.Tasks;

namespace BugSharp
{
    /// <summary>
    /// The service to create components
    /// </summary>
    public interface IComponentService
    {
        /// <summary>
        /// This allows you to create a new component in Bugzilla. You must be authenticated and be in the editcomponents group to perform this action.
        /// </summary>
        /// <param name="component">The component to create</param>
        /// <returns>ID of the new component if successful, otherwise -1</returns>
        Task<int> CreateComponentAsync(Component component);
    }
}