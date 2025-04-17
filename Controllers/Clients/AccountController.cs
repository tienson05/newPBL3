using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Mapper;
using HeThongMoiGioiDoCu.Models;
using HeThongMoiGioiDoCu.Repository.UserRepo;
using HeThongMoiGioiDoCu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            if (user == null || user.Role == 1 || user.Role == 4)
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

        //[Authorize]
        [HttpPost("registerseller")]
        public async Task<IActionResult> RegisterSeller()
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _clientRepository.RegisterSeller(Convert.ToInt32(userId));
            return Ok();
        }

        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var userId = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _clientRepository.GetUserByIdAsync(Convert.ToInt32(userId));
            if (user == null)
            {
                return NotFound("User not found");
            }

            string hashedCurrentPassword = _accountService.HashPassword(resetPasswordDto.CurrentPassword);

            if (!_accountService.VerifyPassword(user.PasswordHash, hashedCurrentPassword))
            {
                return BadRequest("Password is incorrect");
            }
            await _clientRepository.ResetPasswordAsync(resetPasswordDto.NewPassword, Convert.ToInt32(userId));
            return Ok();
        }


        [HttpPost("gentoken")]
        public IActionResult GenerateTestToken([FromBody] GenerateTokenRequest request)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YTE4MTAxNWUyZGI0YzE0YmJlNWEyZjNkYTgyNzc1MTg2NWQzNGI1NTg1NzkwYTgyN2NjZjkwYjRhZDNkYjc2YQ=="));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, request.UserID.ToString()),
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, request.Role)
        };

            var token = new JwtSecurityToken(
                issuer: "yourapp.com",
                audience: "yourapp.com",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = tokenString });
        }
    }


    public class GenerateTokenRequest
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Role { get; set; } = "User";
    }
}
