using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Interfaces
{
    public interface IUserRepository
    {
        Task<Users> GetUserByEmailAsync(string email);
        Task<Users> GetUserByIdAsync(int id);
        Task<List<Users>> GetAllUserAsync();
        Task AddUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task UpdateLastLoginAsync(int id);
        Task<List<Users>> SearchUserAsync(Users user);
        Task ResetPasswordAsync(string newPassword, int id);
    }
}
