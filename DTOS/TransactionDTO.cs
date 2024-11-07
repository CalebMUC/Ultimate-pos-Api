using System.ComponentModel.DataAnnotations;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.DTOS
{
    public class TransactionDto
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public double TotalValueAddedTax { get; set; }
        public double TotalCost { get; set; }
        public double TotalDiscount { get; set; }
        public double AmountRecieved { get; set; }
        public double CashChange { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public List<PaymentDetailsDto> PaymentDetails { get; set; }
        public List<TransactionProductsDTO> products { get; set; }
        public string PaymentConfirmation { get; set; }
        public List<TransactionProductsDTO> Transactionproducts { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        public string UpdatedBy { get; set; } = string.Empty;

    }

    public class PaymentDetailsDto
    {
        public int PaymentID { get; set; }
        public string PaymentReference { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }

    public class TransactionListDto
    {
        [Required]
        public IList<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}
