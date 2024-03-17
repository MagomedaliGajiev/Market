using Example.Models.DTO;

namespace Example.Abstractions
{
    public interface IProductRepository
    {
        public int AddProductCategory(ProductCategoryModel category);
        public IEnumerable<ProductCategoryModel> GetProductCategories();
        public int AddProduct(ProductModel product);
        public IEnumerable<ProductModel> GetProducts();
    }
}
