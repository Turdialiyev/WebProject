using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor_Temp.Model
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Name")]
        [Range(1, 1000, ErrorMessage ="Display Order must be between 1-1000")]
        public int DisplayOrder { get; set; }
    }
}
