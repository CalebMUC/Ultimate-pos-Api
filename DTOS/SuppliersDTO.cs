using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.DTOS
{
    public class SuppliersDTO
    {

        [Required]
        public string SupplierId { get; set; } = string.Empty;

        [Required]
        public string SupplierName { get; set; } = string.Empty;

        [Required]
        public string SupplierType { get; set; } = string.Empty;

        [Required]
        public string Industry { get; set; } = string.Empty;

        [Required]
        public string KRAPIN { get; set; } = string.Empty;

        [Required]
        public string BusinessLicenseNumber { get; set; } = string.Empty;

        [Required]
        public bool SupplierStatus { get; set; }

        public string Remarks { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public string UpdatedBy { get; set; } = string.Empty;

        public List<ContactDetailsDto>? Contactdetails { get; set; }

        public List<AddressDetailsDto>? AddressDetails { get; set; }

        // public List<PaymentDetailDto>? PaymentDetails { get; set; } 

        public List<BankAccountDto>? BankAccountDetails { get; set; }
        public List<MpesaDto>? MpesaDetails { get; set; }

        public List<ProductsDetailsDto>? Productsdetails { get; set; }

        public List<ContractDetailsDto>? ContractDetails { get; set; }

    }



    public class ContactDetailsDto
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

    }

    public class AddressDetailsDto
    {
        public string LocationName { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty;

        public string Postal { get; set; } = string.Empty;

    }

    public class ContractDetailsDto
    {

        public string ContractStartDate { get; set; } = string.Empty;

        public string ContractEndDate { get; set; } = string.Empty;

        public string Terms { get; set; } = string.Empty;

    }



    public class ProductsDetailsDto
    {
        public string? ProductName { get; set; }
        public string? Category { get; set; }
        public string? UnitMeasure { get; set; }
        // public string? Count { get; set; }
    }


    // public class PaymentDetailDto
    // {
    //     public int Id { get; set; }
    //     public List<BankAccountDto>? BankAccount { get; set; }
    //     public List<MpesaDto>? MpesaDtos { get; set; }

    // }
    [NotMapped]
    public class BankAccountDto
    {
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
    }

    [NotMapped]
    public class MpesaDto
    {
        // public List<PaybillDto>? Paybilldto { get; set; }

        public string? Till { get; set; }
        public string? Pochi { get; set; }
    }
    [NotMapped]

    public class PaybillDto
    {

        public int Id { get; set; }
        public string? BusinessNumber { get; set; }
        public string? Account { get; set; }
    }



    public class SuppliersDetilsDTO
    {
        [Required]
        public IList<SuppliersDTO> Supplier { get; set; } = new List<SuppliersDTO>();
    }



}