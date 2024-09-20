using System.ComponentModel.DataAnnotations;

namespace Ultimate_POS_Api.Models
{
    public class Payments
    {
        [Key]
        public int PaymentMethodID { get; set; } // Unique ID for payment method

        public string PaymentReference { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Navigation property for related transactions (if needed)
        public ICollection<Transactions> Transactions { get; set; }
    }
}
