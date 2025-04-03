using System.Security.Claims;
using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Mapper;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeThongMoiGioiDoCu.Controllers.Admin
{
    [Authorize]  // Đảm bảo rằng yêu cầu này chỉ được thực hiện khi người dùng đã có token hợp lệ
    [Route("api/admin/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AccountService _accountService;
        private readonly JwtTokenProviderService _jwtTokenProviderService;
        public AccountController(IUserRepository userRepository, AccountService accountService, JwtTokenProviderService jwtTokenProviderService)
        {
            _userRepository = userRepository;
            _accountService = accountService;
            _jwtTokenProviderService = jwtTokenProviderService;
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
                var token = _jwtTokenProviderService.GenerateToken(user.Name, user.UserID, user.Role);
                return Ok(new {token, user = user.MapToUserViewDto()});
            }

            return Unauthorized("Invalid password");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutUserDto logoutUserDto)
        {
            await _userRepository.UpdateLastLogin(logoutUserDto.UserID);
            return Ok(new { message = "Logged out successfully" }); 
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
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

            if (user.Status == "Banned" || user.Status == "Inactive")
            {
                return BadRequest("User not actived");
            }

            return Ok(user.MapToUserViewDto());
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto createUserDto)
        {
            var hashedPassword = _accountService.HashPassword(createUserDto.Password);
            await _userRepository.AddUserAsync(createUserDto.MapToUser(hashedPassword));

            return Ok(new { message = "Creating user successfully" });
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> GetUserToUpdate([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(user.MapToUpdateUserOfAdminDto());
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromForm] UpdateUserOfAdminDto updateUserOfAdminDto,
            [FromRoute] int id
        )
        {
            await _userRepository.UpdateUserOfAdmin(updateUserOfAdminDto.MapToUser(id));
            return NoContent();
        }
    }
}
