namespace GraphQl.Models.Dto
{
    public class ProductStockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public uint Quantity { get; set; } // Количество товара на складе
    }
}
