using System.ComponentModel.DataAnnotations;

namespace Razor_project.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Name is too long. ")]
        public string Name { get; set; }
    }
}
