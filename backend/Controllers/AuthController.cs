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
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            
            if (await UserExists(userRegisterDto.Email))
            {
                return BadRequest("Email in use!");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password);

            User user = userMapper.UserRegisterDtoToUserWithPasswordHash(userRegisterDto, passwordHash);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            string? userPassword = await _context.Users
                .Where(u => u.Email == userLoginDto.Email)
                .Select(u => u.Password)
                .FirstOrDefaultAsync();

            if (userPassword == null || !BCrypt.Net.BCrypt.Verify(userLoginDto.Password, userPassword))
            {
                return BadRequest("Email or password is incorrect.");
            }
                
            // Making room for expansion for Expiration & Refresh Token in future.

            JwtSecurityToken token = CreateJwtToken(userLoginDto);

            var response = new LoginResponse
            {
                TokenType = "Bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return Ok(response);
        }

        private JwtSecurityToken CreateJwtToken(UserLoginDto loginDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // TODO: Add user types.
            var claims = new[]
{
                new Claim(ClaimTypes.Email, loginDto.Email),
                new Claim(ClaimTypes.Role, "Customer")
            };

            // TODO: When frontend is ready, decrease time.
            var token = new JwtSecurityToken(_jwtIssuer,
                _jwtAudience,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return token;
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
