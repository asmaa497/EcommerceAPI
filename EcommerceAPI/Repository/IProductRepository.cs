using EcommerceAPI.Models;
using System.Collections.Generic;

namespace EcommerceAPI.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();
        public Product GetProduct(int id);
        public List<Product> GetProductsByCatID(int id);
        public Product EditProduct(int id, Product product);
        public void Add(Product product);
    }
}
