using backend.DTO;
using backend.Models;

namespace backend.Mapper
{
    public class UserMapper
    {
        public User MapUserFromUserRegisterDto(UserRegisterDto dto, String passwordHash, string emailToken)
        {
            User user = new User()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = passwordHash,
                IsEmailConfirmed = false,
                EmailVerificationToken = emailToken
            };

            return user;
        }
    }
}
