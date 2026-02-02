using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;


        public System.Text.Json.JsonDocument? Specifications { get; set; }
    }
}