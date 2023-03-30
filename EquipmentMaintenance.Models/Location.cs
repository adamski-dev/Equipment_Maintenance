using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Equipment Location")]
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Location be between 3 and 30 character in length.")]
        public string Name { get; set; }
    }
}
