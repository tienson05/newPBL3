using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Interfaces
{
    public interface IAdminRepository : IUserRepository
    {
        Task DeleteUserAsync(int id);
    }
}
