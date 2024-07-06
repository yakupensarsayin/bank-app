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

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BankDbContext _context;
        private readonly UserMapper userMapper = new UserMapper();

        public AuthController(BankDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            string? userPassword = await _context.Users
                .Where(u => u.Email == userLoginDto.Email)
                .Select(u => u.Password)
                .FirstOrDefaultAsync();

            if (userPassword == null)
            {
                return BadRequest("Email or password is incorrect.");
            }

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, userPassword))
            {
                return BadRequest("Email or password is incorrect.");
            }

            return Ok();
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
