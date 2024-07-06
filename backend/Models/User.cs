using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public partial class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

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
    [StringLength(72)]
    public string Password { get; set; } = null!;
}
