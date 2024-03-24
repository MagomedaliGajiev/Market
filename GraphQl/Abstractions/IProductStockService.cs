using GraphQl.Models.Dto;

namespace GraphQl.Abstractions
{
    public interface IProductStockService
    {
        int AddProductStock(ProductStockDto productStock);
        IEnumerable<ProductStockDto> GetProductStocks();
    }
}
