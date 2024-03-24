using GraphQl.Abstractions;
using GraphQl.Models.Dto;

namespace GraphQl.Query
{
    public class MySimpleQuery
    {
        public IEnumerable<ProductDto> GetProducts([Service] IProductService service) => service.GetProducts();
        public IEnumerable<CategoryDto> GetCategories([Service] ICategoryService service) => service.GetCategories();
        public IEnumerable<StorageDto> GetStorages([Service] IStorageService service) => service.GetStorages();
    }
}
