namespace Example.Models
{
    public class ProductCategory : BaseModel
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();

    }
}
