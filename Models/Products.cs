using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultimate_POS_Api.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public string ProductDescription { get; set; } = string.Empty;

        public string ProductType { get; set; }

        [Required]
        public string ProductCategory { get; set; } = string.Empty;

        // Foreign Key for Categories
        [Required]
        public int CategoryID { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public decimal BuyingPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int Quantity { get; set; }

        // Navigation property
        [ForeignKey("CategoryID")]
        public Categories Categories { get; set; }
    }

    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        // Navigation property for related products
        public ICollection<Products> Products { get; set; }
    }
}
