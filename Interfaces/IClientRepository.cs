using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Interfaces
{
    public interface IClientRepository : IUserRepository
    {
        Task RegisterSeller(int id);
    }
}
