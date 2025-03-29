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
            var existingEmail = await _userRepository.GetUserByEmailAsync(signupDto.Gmail);

            if(existingEmail != null)
            {
                return Conflict("User with this email already exists.");
            }

            var hashedPassword = _accountService.HashPassword(signupDto.Password);

            var user = new User
            {
                Username = signupDto.Username,
                Gmail = signupDto.Gmail,
                PasswordHash = hashedPassword,
                Gender = signupDto.Gender,
                Address = signupDto.Address,
                BirthOfDate = signupDto.BirthOfDate,
                Name = signupDto.Name,
                PhoneNumber = signupDto.PhoneNumber,
                AvatarUrl = signupDto.AvatarUrl,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                IsVerified = true,
                Role = "Buyer",
                Balance = 0,
                TotalPosts = 0,
                TotalPurchases = 0,
                Rating = 0,
                Status = "Active",
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
            
            return Ok(new { message = "Logged out successfully!" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) {
                return NotFound("User not found");
            }

            if(user.Status == "Inactive")
            {
                return BadRequest("User not actived");
            }

            return Ok(new UserViewDto
            {
                UserID = user.UserID,
                Gmail = user.Gmail,
                Username = user.Username,
                Name = user.Name,
                Gender = user.Gender,
                BirthOfDate = user.BirthOfDate,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
                UpdateAt = user.UpdateAt,
                CreateAt = user.CreateAt,
            });
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> GetUserInfor([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not exist");

            return Ok(new UpdateUserDto
            {
                Username = user.Username,
                Gmail = user.Gmail,
                Name= user.Name,
                Gender = user.Gender,
                BirthOfDate = user.BirthOfDate,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, [FromRoute] int id)
        {
            var user = new User
            {
                UserID = id,
                Username = updateUserDto.Username,
                Gmail = updateUserDto.Gmail,
                Name = updateUserDto.Name,
                Gender = updateUserDto.Gender,
                BirthOfDate = updateUserDto.BirthOfDate,
                PhoneNumber = updateUserDto.PhoneNumber,
                Address = updateUserDto.Address,
                AvatarUrl = updateUserDto.AvatarUrl,
            };

            await _userRepository.UpdateUserAsync(user);

            return NoContent();
        }  
    }
}
