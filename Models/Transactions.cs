using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultimate_POS_Api.Models
{
    public class Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        //primary key in Userts Table
        [ForeignKey("User")]
        public int UserID { get; set; }
       //navigation properties
        public User User { get; set; }
        public double TotalValueAddedTax { get; set; }
        public double TotalCost { get; set; } = 0;
        public double TotalDiscount { get; set; }
        public double AmountRecieved { get; set; }
        public double CashChange { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        // Foreign key to PaymentDetails
        [ForeignKey("Payments")]
        public int PaymentMethodID { get; set; }

        // Navigation property for PaymentDetails
        public PaymentDetails PaymentDetails { get; set; }
        public string PaymentConfirmation { get; set; } = string.Empty;
        public DateTime TransactionDateDate { get; set; }
        public string ProductsJson { get; set; } // Store the products as JSON in the database

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        
        // Many-to-many relationship via TransactionProduct join table
        public ICollection<TransactionProduct> TransactionProducts { get; set; }
    }
  

 
}
