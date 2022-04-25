using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Models
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public float price { get; set; }
        public string img { get; set; }
        public int catID { get; set; }
        public DateTime? date { get; set; }
        [JsonIgnore]
        [ForeignKey("catID")]
        public virtual Category Category { get; set; }
        public Product()
        {
            date = DateTime.Now;
        }

    }
}
