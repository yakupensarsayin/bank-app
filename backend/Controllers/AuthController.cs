using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.DTO;
using backend.Mapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using backend.Authentication;
using System.Security.Cryptography;
using NuGet.Common;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BankDbContext _context;
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        private readonly UserMapper userMapper = new UserMapper();

        public AuthController(BankDbContext context, IConfiguration configuration)
        {
            _context = context;
            _jwtKey = configuration["Jwt:Key"]!;
            _jwtIssuer = configuration["Jwt:Issuer"]!;
            _jwtAudience = configuration["Jwt:Audience"]!;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            
            if (await UserExists(dto.Email))
            {
                return BadRequest("Email in use!");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            User user = userMapper.UserRegisterDtoToUserWithPasswordHash(dto, passwordHash);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // TODO: Add role to newly registered user.
             
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            User? user = await _context.Users
                .Where(u => u.Email == dto.Email)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync();

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest("Email or password is incorrect.");
            }

            JwtSecurityToken token = CreateJwtToken(user);

            string refreshToken = GenerateRefreshToken();
            int expiresIn = 3600; // 1 hour, hardcoded

            var response = new TokenResponse
            {
                TokenType = "Bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn
            };

            await SaveRefreshTokenToDatabase(user, refreshToken, expiresIn);

            return Ok(response);
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshModel model)
        {
            TokenValidationResult result = await ValidateExpiredToken(model.AccessToken);

            if (!result.IsValid)
                return Unauthorized();

            string email = result.ClaimsIdentity.FindFirst(ClaimTypes.Email)!.Value;

            User? user = await _context.Users
                .Where(u => u.Email == email)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync();

            if (user == null || user.RefreshToken != model.RefreshToken || user.TokenExpiry < DateTime.UtcNow)
            {
                return Unauthorized();
            }

            string refreshToken = GenerateRefreshToken();
            int expiresIn = 3600; // 1 hour, hardcoded
            JwtSecurityToken token = CreateJwtToken(user);

            var response = new TokenResponse
            {
                TokenType = "Bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn
            };

            await SaveRefreshTokenToDatabase(user, refreshToken, expiresIn);

            return Ok(response);
        }

        private async Task SaveRefreshTokenToDatabase(User user, string refreshToken, int expiresInSeconds)
        {
            user.RefreshToken = refreshToken;
            user.TokenExpiry = DateTime.UtcNow.AddSeconds(expiresInSeconds);

            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        private async Task<TokenValidationResult> ValidateExpiredToken(string token)
        {
            var validation = new TokenValidationParameters
            {
                ValidIssuer = _jwtIssuer,
                ValidAudience = _jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
                ValidateLifetime = false
            };

            return await new JwtSecurityTokenHandler().ValidateTokenAsync(token, validation);
        }

        private JwtSecurityToken CreateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // I left it this way since we are progressing on a single role basis for now.
            // It can be easily changed in the future as it is in the form of a list
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Roles.FirstOrDefault()!.Name)
            };

            // TODO: When frontend is ready, decrease time.
            var token = new JwtSecurityToken(_jwtIssuer,
                _jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return token;
        }

        private static string GenerateRefreshToken()
        {
            byte[] random = new byte[64];

            RandomNumberGenerator.Create().GetBytes(random);

            return Convert.ToBase64String(random);
        }

        private async Task<bool> UserExists(String email)
        {
            User? user = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user != null;
        }
    }
}
