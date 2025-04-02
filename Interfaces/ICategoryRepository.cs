using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetListCategory();
        Task<Category> AddCategory(Category category);
        Task<Category> GetCategoryById(int ID);
        Task<Category?> UpdateCategory(int ID, string categoryName);
        Task<int?> DeleteCategory(int ID);
    }
}
