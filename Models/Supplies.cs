
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ultimate_POS_Api.Models
{
    public class Supplies
    {
        [Key]   
        [Column(TypeName = "VARCHAR(255)")]
        public string SupplierId { get; set; } = string.Empty;

        [Column(TypeName = "VARCHAR(255)")] 
       public DateTime SuppllyDate { get; set; } = DateTime.Now;

      public string categoriesJson { get; set; }

      //  public string productsJson { get; set; }

         [Required]
      public List<Categories> Categories { get; set; }
        
      //   [Required]
      // public List<Products> products { get; set; }

    }
}   