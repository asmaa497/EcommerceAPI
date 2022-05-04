using EcommerceAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EcommerceAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context context;

        public ProductRepository(Context context)
        {
            this.context = context;
        }

        public void Add(Product product)
        {
            
                context.products.Add(product);
                context.SaveChanges();
                
            
        }

        public Product Delete(int id)
        {
            Product pro = context.products.FirstOrDefault(P => P.id == id);
            context.products.Remove(pro);
            context.SaveChanges();
            return pro;
        }

        public Product EditProduct(int id, Product product)
        {
            Product prodModel =
                        context.products.FirstOrDefault(d => d.id == id);
            prodModel.name = product.name;
            prodModel.quantity = product.quantity;
            prodModel.price = product.price;
            prodModel.img = product.img;
            prodModel.catID = product.catID;
            prodModel.price = product.price;
            context.SaveChanges();
            return context.products.FirstOrDefault(p => p.id == id);

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
