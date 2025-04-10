using System.Threading.Tasks;

namespace BugSharp
{
    public interface IBugService
    {
        Task<Bug> GetBugAsync(int bugId);
    }
}