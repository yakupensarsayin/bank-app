using backend.Authentication;
using backend.Models;
using backend.Services.Abstract;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Identity;
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

        private readonly int _refreshTokenExpirationMinutes = 30;

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
                AccessToken = _jwtSecurityTokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
            };

            return response;
        }

        public string ExtractEmailClaim(TokenValidationResult result)
        {
            return result.ClaimsIdentity.FindFirst(ClaimTypes.Email)!.Value;
        }

        public void SetTokensInsideCookie(TokenResponse response, HttpContext context)
        {
            var tokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationMinutes),
                SameSite = SameSiteMode.Strict
            };

            var refreshTokenOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                Expires = DateTime.UtcNow.AddMinutes(_refreshTokenExpirationMinutes),
                SameSite = SameSiteMode.Strict
            };

            context.Response.Cookies.Append("accessToken", response.AccessToken, tokenOptions);
            context.Response.Cookies.Append("refreshToken", response.RefreshToken, refreshTokenOptions);
        }

        public int GetRefreshTokenExpirationMinutes()
        {
            return _refreshTokenExpirationMinutes;
        }

        public void DeleteAuthenticationCookie(HttpContext context)
        {
            var deletedCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                Expires = DateTime.UtcNow.AddHours(-1),
                SameSite = SameSiteMode.Strict
            };

            context.Response.Cookies.Append("accessToken", "", deletedCookieOptions);
            context.Response.Cookies.Append("refreshToken", "", deletedCookieOptions);
        }

        public bool ExtractTokensFromCookie(HttpContext context, out RefreshModel model)
        {
            context.Request.Cookies.TryGetValue("accessToken", out var at);
            context.Request.Cookies.TryGetValue("refreshToken", out var rt);

            if (string.IsNullOrEmpty(at) && string.IsNullOrEmpty(rt))
            {
                model = new RefreshModel() { AccessToken = "", RefreshToken = "" };
                return false;
            }

            model = new RefreshModel() { AccessToken = at!, RefreshToken = rt! };
            return true;
        }

        public string ExtractEmailClaim(HttpContext context)
        {
            return context.User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        }

        public string GenerateEmailVerificationToken()
        {
            byte[] random = new byte[16];

            RandomNumberGenerator.Create().GetBytes(random);

            return Convert.ToHexString(random);
        }
    }
}
