using HeThongMoiGioiDoCu.Models;
using Microsoft.AspNetCore.Identity;

namespace HeThongMoiGioiDoCu.Services
{

    public class AccountService
    {
        private readonly PasswordHasher<Users> _passwordHasher;

        public AccountService()
        {
            _passwordHasher = new PasswordHasher<Users>();
        }

        public string HashPassword(string password)
        {
            // Mã hóa mật khẩu bằng PBKDF2
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string hashedPassword, string enteredPassword)
        {
            // Kiểm tra mật khẩu
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, enteredPassword);
            return result == PasswordVerificationResult.Success;
        }
    }

}
