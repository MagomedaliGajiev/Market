using AutoMapper;
using GraphQl.Abstractions;
using GraphQl.Models;
using GraphQl.Models.Dto;
using Microsoft.Extensions.Caching.Memory;

namespace GraphQl.Sevices
{
    public class StorageService : IStorageService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public StorageService(AppDbContext context, IMapper mapper, IMemoryCache memoryCache)
        {
            _context = context;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public int AddStorage(StorageDto storage)
        {
            using (_context)
            {
                var storageDb = _mapper.Map<StorageEntity>(storage);
                _context.Storages.Add(storageDb);
                _context.SaveChanges();
                _memoryCache.Remove("storages");
                return storageDb.Id;
            }
        }

        public IEnumerable<StorageDto> GetStorages()
        {
            using (_context)
            {
                if (_memoryCache.TryGetValue("storages", out List<StorageDto> storages))
                {
                    return storages;
                }

                storages = _context.Storages.Select(x => _mapper.Map<StorageDto>(x)).ToList();
                _memoryCache.Set("storages", storages, TimeSpan.FromMinutes(30));
                return storages;
            }
        }
    }
}
