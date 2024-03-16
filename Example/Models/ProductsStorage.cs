namespace Example.Models
{
    public class ProductsStorage : BaseModel
    {
        public virtual List<Product> Products { get; set; }
        public int Amount { get; set; }
    }
}
