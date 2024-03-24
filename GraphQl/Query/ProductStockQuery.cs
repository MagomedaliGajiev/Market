using GraphQl.Abstractions;
using GraphQl.Models.Dto;

namespace GraphQl.Query
{
    public class ProductStockQuery
    {
        public IEnumerable<ProductStockDto> GetProductStocks([Service] IProductStockService service)
        {
            return service.GetProductStocks();
        }
    }
}
