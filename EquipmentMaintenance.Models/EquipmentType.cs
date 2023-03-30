using System.ComponentModel.DataAnnotations;

namespace EquipmentMaintenance.Models
{
    public class EquipmentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 character in length.")]
        public string Name { get; set; }
    }
}
