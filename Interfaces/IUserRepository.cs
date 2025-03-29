using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task UpdateUserOfAdmin(User user);
    }
}
