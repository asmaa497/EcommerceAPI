using EcommerceAPI.Models;
using System.Collections.Generic;

namespace EcommerceAPI.Repository
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();
    }
}
