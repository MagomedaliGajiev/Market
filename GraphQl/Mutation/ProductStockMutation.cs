using GraphQl.Abstractions;
using GraphQl.Models.Dto;

namespace GraphQl.Mutation
{
    public class ProductStockMutation
    {
        public int AddProductStock([Service] IProductStockService service, ProductStockDto productStock)
        {
            return service.AddProductStock(productStock);
        }
    }
}
