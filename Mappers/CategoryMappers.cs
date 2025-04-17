using HeThongMoiGioiDoCu.Dtos.Category;
using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Mappers
{
    public static class CategoryMappers
    {
        public static  CategoryDto MapToCategoryDto (this Category category)
        {
            return new CategoryDto
            {
                CategoryName = category.CategoryName
            };
        }
    }
}
