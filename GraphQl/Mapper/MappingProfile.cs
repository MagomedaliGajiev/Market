using AutoMapper;
using GraphQl.Models;
using GraphQl.Models.Dto;

namespace GraphQl.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();
            CreateMap<StorageEntity, StorageDto>().ReverseMap();
        }
    }
}
