using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public virtual List<Product> Products { get; set; }
    }
}
