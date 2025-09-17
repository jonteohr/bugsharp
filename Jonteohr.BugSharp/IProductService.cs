using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugSharp
{
    public interface IProductService
    {
        Task<List<int>> ListProducts();
        Task<Product> GetProduct(int id);
        Task<Product> GetProduct(string name);
        Task UpdateProduct(Product product);
    }
}