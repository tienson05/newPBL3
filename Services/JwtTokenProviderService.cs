using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeThongMoiGioiDoCu.Services
{
    public class JwtTokenProviderService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationTimeInYears;

        // Tiêm cấu hình từ appsettings.json qua constructor
        public JwtTokenProviderService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"];
            _expirationTimeInYears = int.Parse(configuration["Jwt:ExpirationInYears"]);
        }

        public string GenerateToken(string name, int userId, int roleName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, Convert.ToString(roleName)),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,  // Lấy từ appsettings.json
                audience: _audience,  // Lấy từ appsettings.json
                claims: claims,
                expires: DateTime.Now.AddYears(_expirationTimeInYears),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
