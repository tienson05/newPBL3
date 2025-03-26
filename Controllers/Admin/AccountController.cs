using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.DTOs.Account;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Repository;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HeThongMoiGioiDoCu.Controllers.Admin
{
    [Route("api/admin/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AccountService _accountService;

        public AccountController(IUserRepository userRepository, AccountService accountService)
        {
            _userRepository = userRepository;
            _accountService = accountService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninDto signinDto)
        {
            if (string.IsNullOrEmpty(signinDto.Password))
            {
                return BadRequest("Password is required.");
            }

            if (string.IsNullOrEmpty(signinDto.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _userRepository.GetUserByEmailAsync(signinDto.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (_accountService.VerifyPassword(user.PasswordHash, signinDto.Password))
            {
                return Ok(new { message = "Login successful." });
            }

            return Unauthorized("Invalid password");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Xử lý logout: Xóa token hoặc cookie nếu cần
            return Ok(new { message = "Logged out successfully" }); 
        }
    }
}
