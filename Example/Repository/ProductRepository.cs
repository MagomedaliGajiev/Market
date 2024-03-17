using AutoMapper;
using Example.Abstractions;
using Example.Models;
using Example.Models.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace Example.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductRepository(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _cache = memoryCache;
        }
        public int AddProduct(ProductModel product)
        {

            using (var context = new ProductContext())
            {
                var entityProduct = context.Products.FirstOrDefault(x => x.Name.ToUpper() == product.Name.ToUpper());

                if (entityProduct == null)
                {
                    entityProduct = _mapper.Map<Models.Product>(product);
                    context.Products.Add(entityProduct);
                    context.SaveChanges();
                    _cache.Remove("products");
                }
                return entityProduct.Id;
            }
        }

        public int AddProductCategory(ProductCategoryModel category)
        {
            using (var context = new ProductContext())
            {
                var entityCategory = context.ProductCategories.FirstOrDefault(x => x.Name.ToUpper() == category.Name.ToUpper());
                if (entityCategory == null)
                {
                    entityCategory = _mapper.Map<Models.ProductCategory>(category);
                    context.ProductCategories.Add(entityCategory);
                    context.SaveChanges();
                    _cache.Remove("productCategories");
                }
                return entityCategory.Id; 
            }
        }

        public IEnumerable<ProductCategoryModel> GetProductCategories()
        {
            if (_cache.TryGetValue("productCategories", out List<ProductCategoryModel> productCategories))
            {
                return productCategories;
            }
            using (var context = new ProductContext())
            {
                productCategories = context.ProductCategories.Select(x => _mapper.Map<ProductCategoryModel>(x)).ToList();
                _cache.Set("productCategories", productCategories, TimeSpan.FromMinutes(30));
                return productCategories;
                
            }
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            if (_cache.TryGetValue("products", out List<ProductModel> products))
            {
                return products;
            }
            using (var context = new ProductContext())
            {
                products = context.Products.Select(x => _mapper.Map<ProductModel>(x)).ToList();
                _cache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }
    }
}
