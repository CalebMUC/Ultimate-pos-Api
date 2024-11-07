using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultimate_POS_Api.Models
{
    public class Payments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; } // Optional description of the payment method
        public bool IsActive { get; set; } = true; // To track if the payment method is active
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Creation date

        // Navigation property for related payment details
        public ICollection<PaymentDetails> PaymentDetails { get; set; }
    }

    public class PaymentDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentMethodID { get; set; } // Unique ID for the payment detail (not the method)

        // Foreign key to Payments
        [ForeignKey("Payments")]
        public int PaymentID { get; set; }

        public string PaymentReference { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Navigation property for Payments
        public Payments Payments { get; set; }

        // Navigation property for related transactions (for many-to-many or one-to-many)
        public ICollection<Transactions> Transactions { get; set; }
    }
}
