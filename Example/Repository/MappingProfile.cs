using AutoMapper;
using Example.Models;
using Example.Models.DTO;

namespace Example.Repository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductModel>(MemberList.Destination).ReverseMap();
            CreateMap<ProductCategory, ProductCategoryModel>(MemberList.Destination).ReverseMap();
            CreateMap<ProductsStorage, ProductsStorageModel>(MemberList.Destination).ReverseMap();
        }
    }
}
