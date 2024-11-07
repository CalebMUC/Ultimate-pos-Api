namespace Ultimate_POS_Api.DTOS
{
    public class TransactionProductsDTO
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public double ValueAddedTax { get; set; }
    }
}
