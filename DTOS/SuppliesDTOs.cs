using System.ComponentModel.DataAnnotations;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.DTOS
{
    public class SuppliesDTOs
    {

        [Required]
        public string SupplierId { get; set; } = string.Empty;

        [Required]
        public DateTime SuppllyDate { get; set; } = DateTime.Now;

         [Required]
        public List<Categories> Categories { get; set; }
        
        // [Required]
        // public List<Products> products { get; set; } 

    }

    public class SuppliesDetailsDTO
    {
        [Required]
        public IList<SuppliesDTOs> Supplies { get; set; } = new List<SuppliesDTOs>();
    }

    }

