
namespace Example.Models
{
    public class Product : BaseModel
    {
        public decimal Cost { get; set; }
        public int CategoryId { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }

        public virtual List<ProductsStorage> ProductsStorage { get; set; } = new List<ProductsStorage>();
    }
}
