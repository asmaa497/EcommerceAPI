using EcommerceAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Repository
{
    public class CategoryRepository : ICategoryRepository

    {
        private readonly Context context;

        public CategoryRepository(Context context)
        {
            this.context = context;
        }
        public List<Category> GetAll()
        {
            return this.context.categories.ToList();
        }
        
    }
}
