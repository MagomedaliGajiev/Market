using GraphQl.Abstractions;
using GraphQl.Models;
using GraphQl.Models.Dto;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace GraphQl.Services
{
    public class ProductStockService : IProductStockService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ProductStockService(AppDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public int AddProductStock(ProductStockDto productStock)
        {
            var productStockDb = _mapper.Map<ProductStock>(productStock);
            _context.ProductStocks.Add(productStockDb);
            _context.SaveChanges();
            return productStockDb.Id;
        }

        public IEnumerable<ProductStockDto> GetProductStocks()
        {
            var productStocks = _context.ProductStocks.ToList();
            return _mapper.Map<List<ProductStockDto>>(productStocks);
        }
    }
}