namespace GraphQl.Models
{
    public class ProductStock
    {
        public int Id { get; set; }
        public virtual ProductEntity Product { get; set; }
        public int ProductId { get; set; }
        public virtual StorageEntity Storage { get; set; }
        public int StorageId { get; set; }
        public uint Quantity { get; set; } // Количество товара на складе
    }
}
