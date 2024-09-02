using System.Collections.Generic; // Import for IList
using System.ComponentModel.DataAnnotations;

namespace Ultimate_POS_Api.DTOS
{
    public class ProductDTOs
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string ProductDescription { get; set; } = string.Empty;

        public string ProductType { get; set; }

        [Required]
        public string ProductCategory { get; set; } = string.Empty;

        // Only pass the CategoryID in the DTO
        [Required]
        public int CategoryID { get; set; }

        public decimal BuyingPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int Quantity { get; set; }
    }

    public class ProductListDto
    {
        [Required]
        public IList<ProductDTOs> Products { get; set; } = new List<ProductDTOs>();
    }
}
