using HeThongMoiGioiDoCu.Dtos.Account;
using HeThongMoiGioiDoCu.Interfaces;
using HeThongMoiGioiDoCu.Mapper;
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

            var user = signupDto.MapToUser(hashedPassword);

            await _userRepository.AddUserAsync(user);

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

            if(user == null || user.Role == "Admin")
            {
                return NotFound("User not found!");
            }

            if (_accountService.VerifyPassword(user.PasswordHash, signinDto.Password))
            {
                return Ok(user.MapToUserViewDto());
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

            return Ok(user.MapToUserViewDto());
        }

        [HttpGet("update/{id}")]
        public async Task<IActionResult> GetUserInfor([FromRoute] int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not exist");

            return Ok(user.MapToUpdateUserDto());
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto, [FromRoute] int id)
        {
            //var user = UserMappers.MapToUser(updateUserDto, id);

            var user = updateUserDto.MapToUser(id);
            await _userRepository.UpdateUserAsync(user);

            return NoContent();
        }  
    }
}
