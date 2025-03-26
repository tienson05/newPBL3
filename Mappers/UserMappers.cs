using HeThongMoiGioiDoCu.DTOs.Account;
using HeThongMoiGioiDoCu.Models;
using System.Runtime.CompilerServices;

namespace HeThongMoiGioiDoCu.Mapper
{
    public static class UserMappers
    {
        public static SignupDto ToUserDto(this User userModel)
        {
            return new SignupDto
            {
                Username = userModel.Username,
                Password = userModel.PasswordHash,
                Email = userModel.Email
            };
        }
    }
}
