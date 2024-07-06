using backend.DTO;
using backend.Models;

namespace backend.Mapper
{
    public class UserMapper
    {
        public User UserRegisterDtoToUserWithPasswordHash(UserRegisterDto dto, String passwordHash)
        {
            User user = new User()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Password = passwordHash
            };

            return user;
        }
    }
}
