using System.ComponentModel.DataAnnotations;

namespace Ultimate_POS_Api.DTOS
{
    public class CategoryDTO
    {
        [Required]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public string CategoryCode { get; set; } = string.Empty;

        [Required]
        public string NoOfItems { get; set; }

        [Required]
        public string CategoryDescription { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; }

        public string CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
    }
    public class CategoryListDto
    {
        [Required]
        public IList<CategoryDTO> Categ { get; set; } = new List<CategoryDTO>();
    }
}
