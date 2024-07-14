using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class AccountStatement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime StatementDate { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public int LastBalance { get; set; }

        public enum TransactionType
        {
            Sender,
            Receiver
        }

        public Account Account { get; set; } = null!;
    }
}