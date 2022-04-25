using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Models
{

    public class Context :IdentityDbContext<ApplicationUser>
    {
        public Context()
        {

        }
        public Context(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product> products { set; get; }
        public DbSet<Category> categories { get; set; }

    }
}
