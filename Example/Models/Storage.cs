namespace Example.Models
{
    public class Storage : BaseModel
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual List<ProductsStorage> ProductsStorage { get; set; } = new List<ProductsStorage>();
    }
}