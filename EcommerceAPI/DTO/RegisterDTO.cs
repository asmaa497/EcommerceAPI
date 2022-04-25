using EcommerceAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string fullName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }
        public Address address { get; set; } = new Address();
        public List<string> mobileNum { get; set; } = new List<string>();
        public string deliveryOptions { get; set; }
        public string specificDays { get; set; }
    }
}
