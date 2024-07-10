using backend.DTO;
using backend.Models;

namespace backend.Services.Abstract
{
    public interface IUserService
    {
        public Task<bool> DoesUserExist(string email);
        public Task<User> GetUserWithRoles(string email);
        public Task SaveRefreshTokenToDatabase(User user, string refreshToken, int expiresInSeconds);
        public Task RegisterUserToDatabase(UserRegisterDto dto, string passwordHash, Role customerRole);
    }
}
