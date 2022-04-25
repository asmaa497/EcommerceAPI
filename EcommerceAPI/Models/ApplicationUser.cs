using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EcommerceAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        public Address address { get; set; } = new Address();
        public List<Mobiles> mobileNum { get; set; } = new List<Mobiles>();
        public string deliveryOptions { get; set; }
        public string specificDays { get; set; }

    }
}
