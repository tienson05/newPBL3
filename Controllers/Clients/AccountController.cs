using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Mapper;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeThongMoiGioiDoCu.Controllers.Clients
{

    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly AccountService _accountService;
        private readonly JwtTokenProviderService _jwtTokenProviderService;

        public AccountController(AccountService accountService, JwtTokenProviderService jwtTokenProviderService, IClientRepository clientRepository)
        {
            _accountService = accountService;
            _jwtTokenProviderService = jwtTokenProviderService;
            _clientRepository = clientRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromForm] SignupDto signupDto)
        {
            var existingEmail = await _clientRepository.GetUserByEmailAsync(signupDto.Gmail);

            if (existingEmail != null)
            {
                return Conflict("User with this email already exists.");
            }

            var hashedPassword = _accountService.HashPassword(signupDto.Password);

            var user = signupDto.MapToUser(hashedPassword);

            await _clientRepository.AddUserAsync(user);

            return Ok("Signup successfully!");
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] SigninDto signinDto)
        {
            if (string.IsNullOrWhiteSpace(signinDto.Email))
            {
                return BadRequest("Email is required hehrhrh!");
            }

            if (string.IsNullOrWhiteSpace(signinDto.Password))
            {
                return BadRequest("Password is required!");
            }

            var user = await _clientRepository.GetUserByEmailAsync(signinDto.Email);

            if (user == null || user.Role == 1)
            {
                return NotFound("User not found!");
            }

            if (_accountService.VerifyPassword(user.PasswordHash, signinDto.Password))
            {
                var token = _jwtTokenProviderService.GenerateToken(user.Name, user.UserID, user.Role);
                return Ok(new { token, user = user.MapToUserViewDto() });
            }

            return Unauthorized("Invalid password!");
        }

        [Authorize]
        [HttpPost("logout")] 
        public async Task<IActionResult> Logout()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _clientRepository.UpdateLastLoginAsync(Convert.ToInt32(userId));
            return Ok(new { message = "Logged out successfully!" });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var user = await _clientRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.Status == "Inactive")
            {
                return BadRequest("User not actived");
            }

            return Ok(user.MapToUserViewDto());
        }


        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            //var userName = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            //var userRole = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = updateUserDto.MapToUser(Convert.ToInt32(userId));
            await _clientRepository.UpdateUserAsync(user);

            return Ok("Updated successfully!");
        }

        //[Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchUser([FromQuery] SearchUserDto searchUserDto) {
            if (searchUserDto == null)
            {
                return BadRequest("Invalid search parameters.");
            }
            
            List<Users> users = await _clientRepository.SearchUserAsync(searchUserDto.MapToUser());
            List<UserViewDto> userList = new List<UserViewDto>();
            if (users.Count == 0) return NotFound("Users not found!");
            else
            {
                foreach (var user in users)
                {
                    userList.Add(user.MapToUserViewDto());
                }
            }
            return Ok(userList.ToList());
        }
    }
}
