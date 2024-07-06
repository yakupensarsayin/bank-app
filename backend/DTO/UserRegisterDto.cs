using System.ComponentModel.DataAnnotations;

namespace backend.DTO
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Surname { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string Password { get; set; } = null!;
    }
}
