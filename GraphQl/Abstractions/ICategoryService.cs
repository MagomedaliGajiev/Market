using GraphQl.Models.Dto;

namespace GraphQl.Abstractions
{
    public interface ICategoryService
    {
        int AddCategory(CategoryDto category);
        IEnumerable<CategoryDto> GetCategories();
    }
}
