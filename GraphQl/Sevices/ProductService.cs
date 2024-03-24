using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Models;
using GraphQl.Models.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace GraphQl.Sevices
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ProductService(AppDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public int AddProduct(ProductDto product)
        {
            using (_context)
            {
                var productDb = _mapper.Map<ProductEntity>(product);
                _context.Products.Add(productDb);
                _context.SaveChanges();
                _memoryCache.Remove("products");
                return productDb.Id;
            }
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            using (_context)
            {
                if (_memoryCache.TryGetValue("products", out List<ProductDto> products))
                {
                    return products;
                }

                products = _context.Products.Select(x => _mapper.Map<ProductDto>(x)).ToList();

                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }
    }
}
