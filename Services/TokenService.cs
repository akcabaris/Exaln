using Exaln.DTO.AccountDTO;
using Exaln.Entities;
using Exaln.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Exaln.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRedisService _redis;

        public TokenService(IConfiguration configuration, IRedisService redis)
        {
            _configuration = configuration;
            _redis = redis;
        }

        public async Task<AuthResponseDTO> CreateTokenAsync(ApplicationUser user, HttpResponse response)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("token_version", user.TokenVersion.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.UtcNow.AddMinutes(15),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = GenerateRefreshToken();

            short expireDays = short.Parse(_configuration["JwtSettings:RefreshTokenExpireDays"]!);
            var refreshTtl = TimeSpan.FromDays(expireDays);

            var refreshKey = $"refresh:{refreshToken}";
            var userTokensKey = $"user:{user.Id}:tokens";
            var versionKey = $"user:{user.Id}:token_version";


            await _redis.SetStringAsync(refreshKey, user.Id, refreshTtl);

            await _redis.AddToSetAsync(userTokensKey, refreshToken);

            await _redis.ExpireAsync(userTokensKey, refreshTtl);

            await _redis.SetStringAsync(versionKey, user.TokenVersion.ToString());

            response.Cookies.Append(
                "refresh_token",
                refreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(expireDays)
                }
                );

            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                User = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email!,
                    FirstName = "",
                    LastName = ""
                }
            };
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            RandomNumberGenerator.Fill(randomBytes);

            return Convert.ToBase64String(randomBytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
    }
}
