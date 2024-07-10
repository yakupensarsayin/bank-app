using backend.DTO;
using backend.Mapper;
using backend.Models;
using backend.Services.Abstract;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Concrete
{
    public class UserManager : IUserService
    {
        private readonly BankDbContext _context;

        private readonly UserMapper userMapper = new UserMapper();

        public UserManager(BankDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DoesUserExist(string email)
        {
            User? user = await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();

            return user != null;
        }

        public async Task<User> GetUserWithRoles(string email)
        {
            User? user = await _context.Users
                .Where(u => u.Email == email)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync();

            // We are making checks on the returned result
#pragma warning disable CS8603
            return user;
#pragma warning restore CS8603
        }

        public async Task SaveRefreshTokenToDatabase(User user, string refreshToken, int expiresInSeconds)
        {
            user.RefreshToken = refreshToken;
            user.TokenExpiry = DateTime.UtcNow.AddSeconds(expiresInSeconds);

            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task RegisterUserToDatabase(UserRegisterDto dto, string passwordHash, Role customerRole)
        {
            User user = userMapper.MapUserFromUserRegisterDto(dto, passwordHash);

            user.Roles = new List<Role>();
            user.Roles.Add(customerRole);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
