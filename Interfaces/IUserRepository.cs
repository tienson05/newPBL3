﻿using HeThongMoiGioiDoCu.Models;

namespace HeThongMoiGioiDoCu.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
    }

}
