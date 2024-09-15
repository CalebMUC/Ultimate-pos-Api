using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultimate_POS_Api.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type with a max length
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "TEXT")] // Use TEXT for longer descriptions
        public string ProductDescription { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] // Optional ProductType
        public string ProductType { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(255)")] // ProductCategory with a max length
        public string ProductCategory { get; set; } = string.Empty;

        // Foreign Key for Categories
        [Required]
        public int CategoryID { get; set; }

        [Column(TypeName = "DATETIME")] // MySQL-compatible DateTime
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "DATETIME")] // MySQL-compatible DateTime
        public DateTime UpdatedOn { get; set; }

        [Column(TypeName = "VARCHAR(255)")] // Use VARCHAR for names
        public string CreatedBy { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "DECIMAL(10, 2)")] // Fixed precision for currency
        public decimal BuyingPrice { get; set; }

        [Column(TypeName = "DECIMAL(10, 2)")]
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
        [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type with a max length
        public string CategoryName { get; set; } 

              [Required]
        [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type with a max length
        public string CategoryCode { get; set; } 

              [Required]
        [Column(TypeName = "NVARCHAR(255)")] // MySQL-compatible string type with a max length
        public string NoOfItems { get; set; } 

         [Required]
        [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type with a max length
        public string CategoryDescription { get; set; } 

          [Required]
        [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type with a max length
        public string Status { get; set; } 


        [Column(TypeName = "DATETIME")] // MySQL-compatible DateTime
        public DateTime CreatedOn { get; set; }

        [Column(TypeName = "DATETIME")] // MySQL-compatible DateTime
        public DateTime UpdatedOn { get; set; }

        [Column(TypeName = "VARCHAR(255)")] // Use VARCHAR for names
        public string CreatedBy { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string UpdatedBy { get; set; }

        // Navigation property for related products
        public ICollection<Products> Products { get; set; }
    }
}
