using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RateLimit_WebAPI
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [JsonConstructor]
        public Product(int productId, string name, string description, decimal price)
        {
            this.ProductId = productId;
            this.Name = name;
            this.Description = description;
            this.Price = price;
        }
    }
}
