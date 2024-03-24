using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Models;
using GraphQl.Models.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace GraphQl.Sevices
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public CategoryService(AppDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public int AddCategory(CategoryDto category)
        {
            using (_context)
            {
                var categoryDb = _mapper.Map<CategoryEntity>(category);
                _context.Categories.Add(categoryDb);
                _context.SaveChanges();
                _memoryCache.Remove("categories");
                return categoryDb.Id;
            }
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            using (_context)
            {
                if (_memoryCache.TryGetValue("categories", out List<CategoryDto> categories))
                {
                    return categories;
                }
                categories = _context.Categories.Select(x => _mapper.Map<CategoryDto>(x)).ToList();
                _memoryCache.Set("categories", categories, TimeSpan.FromMinutes(30));
                return categories;
            }
        }
    }
}
