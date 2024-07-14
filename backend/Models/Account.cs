using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(34)]
        public string IBAN { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string AccountType { get; set; } = null!;

        [Required]
        public int Balance { get; set; }

        public Customer Customer { get; set; } = null!;

        public List<AccountStatement> AccountStatements { get; set; } = new List<AccountStatement>();
    }
}