using EcommerceAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context context)
        {
            this.context = context;
        }
        public Product GetProduct(int id)
        {
           return this.context.products.FirstOrDefault(P => P.id == id);
        }

        public List<Product> GetProducts()
        {
            return this.context.products.ToList();
        }
        public List<Product> GetProductsByCatID(int id)
        {
            return this.context.products.Where(P => P.catID == id).ToList();
        }

        
    }
}
