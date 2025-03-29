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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id) { 
            await _userRepository.DeleteUserAsync(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.IsActive == false)
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm]  CreateUserDto createUserDto)
        {
            User user = new User()
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                PasswordHash = _accountService.HashPassword(createUserDto.Password),
                Fullname = createUserDto.Fullname,
                Gender = createUserDto.Gender,
                DateOfBirth = createUserDto.DateOfBirth,
                PhoneNumber = createUserDto.PhoneNumber,
                Address = createUserDto.Address,
                ProfilePictureUrl = createUserDto.ProfilePictureUrl,
                IsActive = createUserDto.IsActive,
                Role = createUserDto.Role,
            };
            await _userRepository.AddUserAsync(user);

            return Ok(new { message = "Creating user successfully" });
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> GetUserToUpdate([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null) return NotFound("User not found");

            return Ok(new UpdateUserOfAdminDto
            {
                Username = user.Username,
                Email = user.Email,
                Fullname = user.Fullname,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePictureUrl = user.ProfilePictureUrl,
                IsActive = user.IsActive,
                Role = user.Role,
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserOfAdminDto updateUserOfAdminDto, [FromRoute] int id)
        {
            var user = new User
            {
                Id = id,
                Username = updateUserOfAdminDto.Username,
                Email = updateUserOfAdminDto.Email,
                Fullname = updateUserOfAdminDto.Fullname,
                Gender = updateUserOfAdminDto.Gender,
                DateOfBirth = updateUserOfAdminDto.DateOfBirth,
                PhoneNumber = updateUserOfAdminDto.PhoneNumber,
                Address = updateUserOfAdminDto.Address,
                ProfilePictureUrl = updateUserOfAdminDto.ProfilePictureUrl,
                IsActive = updateUserOfAdminDto.IsActive,
                Role = updateUserOfAdminDto.Role,
            };

            await _userRepository.UpdateUserOfAdmin(user);
            return NoContent();
        }

    }
}
