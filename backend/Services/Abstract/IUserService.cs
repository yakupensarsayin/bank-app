using backend.DTO;
using backend.Models;

namespace backend.Services.Abstract
{
    public interface IUserService
    {
        public Task DeleteRefreshTokenFromDatabase(string email);
        public Task<bool> DoesUserExist(string email);
        public Task<User> GetUser(string email);
        public Task<User> GetUserWithRoles(string email);
        public Task SaveRefreshTokenToDatabase(User user, string refreshToken, int refreshTokenExpiryInMinutes);
        public Task RegisterUserToDatabase(UserRegisterDto dto, string passwordHash, Role customerRole);
    }
}
