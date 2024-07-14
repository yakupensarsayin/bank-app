using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend.Models;

public partial class User
{
    [Key]
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

    public string? RefreshToken { get; set; }

    public DateTime TokenExpiry {  get; set; }

    [StringLength(32)]
    public string? EmailVerificationToken { get; set; }
    public bool IsEmailConfirmed { get; set; } = false;

    public List<Role> Roles { get; set; } = new List<Role>();
}
