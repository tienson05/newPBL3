using Emby.Media.Model.Attributes;
using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.DTOs.Account;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeThongMoiGioiDoCu.Controllers.Clients
{
    [Route("api/account")]
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

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromForm] SignupDto signupDto)
        {
            var existingEmail = await _userRepository.GetUserByEmailAsync(signupDto.Email);

            if(existingEmail != null)
            {
                return Conflict("User with this email already exists.");
            }

            var hashedPassword = _accountService.HashPassword(signupDto.Password);

            var user = new User
            {
                Username = signupDto.Username,
                Email = signupDto.Email,
                PasswordHash = hashedPassword,
                Gender = signupDto.Gender,
                Address = signupDto.Address,
                DateOfBirth = signupDto.DateOfBirth,
                Fullname = signupDto.Fullname,
                PhoneNumber = signupDto.PhoneNumber
            };

            await _userRepository.AddUserAsync(user);

            // Trả về thông báo thành công và URL đăng nhập
            return CreatedAtAction(nameof(Signin), new { controller = "Account" }, new { message = "Registration successful. You can now log in.", loginUrl = "/api/account/signin" });
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromForm] SigninDto signinDto)
        {
            if(string.IsNullOrWhiteSpace(signinDto.Email))
            {
                return BadRequest("Email is required!");
            }

            if(string.IsNullOrWhiteSpace(signinDto.Password))
            {
                return BadRequest("Password is required!");
            }

            var user = await _userRepository.GetUserByEmailAsync(signinDto.Email);

            if(user == null)
            {
                return NotFound("User not found!");
            }

            if (_accountService.VerifyPassword(user.PasswordHash, signinDto.Password))
            {
                return Ok(new { message = "Login successful!" });
            }

            return Unauthorized("Invalid password!");
        }

        [HttpPost("logout")] 
        public async Task<IActionResult> Logout()
        {
            //Xoa token hoac cookie
            return Ok(new { message = "Logged out successfully!" });
        }
    }
}
