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
                PhoneNumber = signupDto.PhoneNumber,
                ProfilePictureUrl = signupDto.ProfilePictureUrl,
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) {
                return NotFound("User not found");
            }

            if(user.IsActive == false)
            {
                return BadRequest("User not actived");
            }

            return Ok(new UserViewDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Fullname = user.Fullname,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Role = user.Role,
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
                Email = user.Email,
                Fullname= user.Fullname,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePictureUrl = user.ProfilePictureUrl,
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, [FromRoute] int id)
        {
            var user = new User
            {
                Id = id,
                Username = updateUserDto.Username,
                Email = updateUserDto.Email,
                Fullname = updateUserDto.Fullname,
                Gender = updateUserDto.Gender,
                DateOfBirth = updateUserDto.DateOfBirth,
                PhoneNumber = updateUserDto.PhoneNumber,
                Address = updateUserDto.Address,
                ProfilePictureUrl = updateUserDto.ProfilePictureUrl,
                UpdateAt = DateTime.UtcNow,
            };

            await _userRepository.UpdateUserAsync(user);

            return NoContent();
        }  
    }
}
