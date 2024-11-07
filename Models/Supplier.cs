using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultimate_POS_Api.Models
{
    public class Supplier
    {
        [Key]   
        [Column(TypeName = "VARCHAR(255)")]
        public string SupplierId { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "VARCHAR(255)")] 
        public string SupplierName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "VARCHAR(255)")]      
        public string SupplierType { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "VARCHAR(255)")] 
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "VARCHAR(255)")] 
        public string KRAPIN { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "VARCHAR(255)")] 
        public string BusinessLicenseNumber { get; set; } = string.Empty;

        [Required]
        public bool SupplierStatus { get; set; }

        public string Remarks { get; set; } = string.Empty; 

        [Required]
        [Column(TypeName = "VARCHAR(255)")] 
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "VARCHAR(255)")] 
        public string UpdatedBy { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "DATETIME")] 
        public DateTime CreatedOn { get; set; }


        public string ContactDetailsJson { get; set; }
        public string? AddressDetailsJson { get; set; }
        public string PaymentDetailsJson { get; set; }  
        public string ProductInfosJson { get; set; }
        public string ContractDetailsJson { get; set; }    
        public string BankDetailsJson {get; set; }
         public string MpesaDetailsJson {get; set; }


         public List<ContactDetail> ContactDetails { get; set; }
        public List<AddressDetail> AddressDetails { get; set; }
        public List<ProductInfo> ProductInfoDetails { get; set; }  
        public List<ContractDetail> ContractDetails { get; set; }  
        public List<BankAccount> BankAccountDetails { get; set; }     
        public List<Mpesa> MpesaDetails { get; set; }     



  
    }

    public class ContractDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string ContractStartDate { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] 
        public string ContractEndDate { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] 
        public string Terms { get; set; } = string.Empty;

        public bool Status { get; set; }
    }

    public class ProductInfo
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string ProductName { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string Category { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string? UnitMeasure { get; set; } 

        // [Column(TypeName = "VARCHAR(255)")] 
        // public string? Count { get; set; }
    }

    public class AddressDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string LocationName { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] 
        public string Town { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] 
        public string Postal { get; set; } = string.Empty;
    }

    public class ContactDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] 
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] 
        public string Phone { get; set; } = string.Empty;
    }

    // public class PaymentDetail
    // {
    //     [Key]
    //     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //     public int Id { get; set; } 

   
       
    // } 

        public class BankAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

          [Column(TypeName = "VARCHAR(255)")] 
        public string BankName { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string AccountNumber { get; set; }

    }

    public class Mpesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string Till { get; set; }

        [Column(TypeName = "VARCHAR(255)")] 
        public string Pochi { get; set; }

        // public Paybill paybill { get; set; }     

    }

    // public class Paybill
    // {
    //     [Key]
    //     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //     public int Id { get; set; }

    //     [Column(TypeName = "VARCHAR(255)")] 
    //     public string BusinessNumber { get; set; }

    //     [Column(TypeName = "VARCHAR(255)")] 
    //     public string Account { get; set; }
    // }
}
