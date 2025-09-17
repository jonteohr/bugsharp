using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp.Services
{
    internal class ProductService : BaseRequestClient, IProductService
    {
        private readonly BugZilla _bugZilla;
        
        public ProductService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }

        public async Task<List<int>> ListProducts()
        {
            var response = await GetAsync(Endpoints.ProductList, -1, _bugZilla.Settings.ApiKey);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, List<int>>>(response);

            if ((!dict.TryGetValue("ids", out var ids)) || ids.Count < 1)
                throw new BugZillaRequestException();
            
            return ids;
        }

        public async Task<Product> GetProduct(int id) => await GetProductGeneric(id);

        public async Task<Product> GetProduct(string name) => await GetProductGeneric(name);
        public async Task UpdateProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            await PutAsync(Endpoints.Product, product.Id, _bugZilla.Settings.ApiKey, json);
        }

        private async Task<Product> GetProductGeneric(object param)
        {
            string response = string.Empty;
            if (param is int i)
            {
                response = await GetAsync(Endpoints.Product, i, _bugZilla.Settings.ApiKey);
            } 
            else if (param is string s)
            {
                response = await GetAsync(Endpoints.Product, s, _bugZilla.Settings.ApiKey);
            }
            
            var dict = JsonConvert.DeserializeObject<ProductResponse>(response);

            if (dict.Products == null || dict.Products.Count < 1)
                throw new BugZillaRequestException();

            return dict.Products.Select(product => new Product(_bugZilla, product)).FirstOrDefault();
        }
    }
}