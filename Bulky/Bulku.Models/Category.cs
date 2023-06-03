using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        [DisplayName("Display Name")]
        [Range(1,1000)]
        public int DisplayOrder { get; set; }
    }
}
