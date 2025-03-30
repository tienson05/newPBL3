using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Mvc;

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
                return Ok(
                    new UserViewDto
                    {
                        UserID = user.UserID,
                        Name = user.Name,
                        Gmail = user.Gmail,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        AvatarUrl = user.AvatarUrl,
                        Username = user.Username,
                        Gender = user.Gender,
                        BirthOfDate = user.BirthOfDate,
                        Balance = user.Balance,
                        TotalPosts = user.TotalPosts,
                        TotalPurchases = user.TotalPurchases,
                        Rating = user.Rating,
                        Role = user.Role,
                        UpdatedAt = user.UpdatedAt,
                        CreatedAt = user.CreatedAt,
                    }
                );
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

            return Ok(
                new UserViewDto
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
                    Role = user.Role,
                    Balance = user.Balance,
                    TotalPosts = user.TotalPosts,
                    TotalPurchases = user.TotalPurchases,
                    IsVerified = user.IsVerified,
                    Rating = user.Rating,
                    UpdatedAt = user.UpdatedAt,
                    CreatedAt = user.CreatedAt,
                }
            );
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto createUserDto)
        {
            User user = new User()
            {
                Username = createUserDto.Username,
                Gmail = createUserDto.Gmail,
                PasswordHash = _accountService.HashPassword(createUserDto.Password),
                Name = createUserDto.Name,
                Gender = createUserDto.Gender,
                BirthOfDate = createUserDto.BirthOfDate,
                PhoneNumber = createUserDto.PhoneNumber,
                Address = createUserDto.Address,
                AvatarUrl = createUserDto.AvatarUrl,
                Status = createUserDto.Status,
                Role = createUserDto.Role,
                IsVerified = createUserDto.IsVerified,
            };
            await _userRepository.AddUserAsync(user);

            return Ok(new { message = "Creating user successfully" });
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> GetUserToUpdate([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            return Ok(
                new UpdateUserOfAdminDto
                {
                    Username = user.Username,
                    Gmail = user.Gmail,
                    Name = user.Name,
                    Gender = user.Gender,
                    BirthOfDate = user.BirthOfDate,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    AvatarUrl = user.AvatarUrl,
                    Status = user.Status,
                    Role = user.Role,
                    IsVerified = user.IsVerified,
                    UserID = user.UserID,
                }
            );
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(
            [FromForm] UpdateUserOfAdminDto updateUserOfAdminDto,
            [FromRoute] int id
        )
        {
            var user = new User
            {
                UserID = id,
                Username = updateUserOfAdminDto.Username,
                Gmail = updateUserOfAdminDto.Gmail,
                Name = updateUserOfAdminDto.Name,
                Gender = updateUserOfAdminDto.Gender,
                BirthOfDate = updateUserOfAdminDto.BirthOfDate,
                PhoneNumber = updateUserOfAdminDto.PhoneNumber,
                Address = updateUserOfAdminDto.Address,
                AvatarUrl = updateUserOfAdminDto.AvatarUrl,
                Status = updateUserOfAdminDto.Status,
                Role = updateUserOfAdminDto.Role,
                IsVerified = updateUserOfAdminDto.IsVerified,
            };

            await _userRepository.UpdateUserOfAdmin(user);
            return NoContent();
        }
    }
}
