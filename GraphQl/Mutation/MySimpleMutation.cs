using GraphQl.Abstractions;
using GraphQl.Models.Dto;

namespace GraphQl.Mutation
{
    public class MySimpleMutation
    {
        public int AddProduct([Service] IProductService service, ProductDto product) 
        {
            var productDb = service.AddProduct(product);
            return productDb;
        }
    }
}
