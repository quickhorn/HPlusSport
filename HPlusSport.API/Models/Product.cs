using System.Text.Json.Serialization;

namespace HPlusSport.API.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }

    }
}
