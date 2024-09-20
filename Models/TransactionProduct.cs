using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Ultimate_POS_Api.Models
{
    public class TransactionProduct
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to Transactions
        [ForeignKey("Transaction")]
        public int TransactionID { get; set; }
        public Transactions Transaction { get; set; }

        // Foreign key to Products table (which already exists)
        [ForeignKey("Product")]
        public string ProductID { get; set; }
        public Products Product { get; set; }

        public int Quantity { get; set; }
    }

}
