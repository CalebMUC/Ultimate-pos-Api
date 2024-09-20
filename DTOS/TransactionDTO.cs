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
        public PaymentDetailsDto PaymentDetails { get; set; }
        public string PaymentConfirmation { get; set; }
        public List<TransactionProductsDTO> Products { get; set; }
    }

    public class PaymentDetailsDto
    {
        public string PaymentMethodID { get; set; }
        public string PaymentReference { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
