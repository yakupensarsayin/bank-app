using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public partial class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime RegisterDate { get; set; }

    [Required]
    [Range(0, 10)]
    public short Credibility { get; set; }

    public User User { get; set; } = null!;

    public List<Account> Accounts { get; set; } = new List<Account>();
}