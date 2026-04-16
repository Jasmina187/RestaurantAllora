using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProjectData.Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
