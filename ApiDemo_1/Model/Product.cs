using System.ComponentModel.DataAnnotations;

namespace ApiDemo_1.Model
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string? Price { get; set; }
    }
}
