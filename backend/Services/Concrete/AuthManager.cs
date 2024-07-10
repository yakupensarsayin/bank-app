using backend.Authentication;
using backend.Models;
using backend.Services.Abstract;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace backend.Services.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly SymmetricSecurityKey _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        // can be changed in future, right now harcoded.
        private readonly int _jwtExpirationMinutes = 15;
        private readonly int _jwtExpirationSeconds = 15 * 60;

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        public AuthManager(IConfiguration configuration)
        {
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            _jwtIssuer = configuration["Jwt:Issuer"]!;
            _jwtAudience = configuration["Jwt:Audience"]!;
        }

        private JwtSecurityToken CreateJwtToken(User user)
        {
            var credentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            // Add all roles of the user.
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            var token = new JwtSecurityToken(_jwtIssuer,
                _jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtExpirationMinutes),
                signingCredentials: credentials);

            return token;
        }

        private static string GenerateRefreshToken()
        {
            byte[] random = new byte[64];

            RandomNumberGenerator.Create().GetBytes(random);

            return Convert.ToBase64String(random);
        }

        public async Task<TokenValidationResult> ValidateExpiredToken(string jwtToken)
        {
            var validation = new TokenValidationParameters
            {
                ValidIssuer = _jwtIssuer,
                ValidAudience = _jwtAudience,
                IssuerSigningKey = _jwtKey,
                ValidateLifetime = false
            };

            return await _jwtSecurityTokenHandler.ValidateTokenAsync(jwtToken, validation);
        }

        public bool VerifyPasswordHash(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public TokenResponse CreateTokenResponse(User user)
        {
            JwtSecurityToken token = CreateJwtToken(user);
            string refreshToken = GenerateRefreshToken();
            int expiresIn = _jwtExpirationSeconds;

            var response = new TokenResponse
            {
                TokenType = "Bearer",
                AccessToken = _jwtSecurityTokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn
            };

            return response;
        }

        public string ExtractEmailClaim(TokenValidationResult result)
        {
            return result.ClaimsIdentity.FindFirst(ClaimTypes.Email)!.Value;
        }

    }
}
