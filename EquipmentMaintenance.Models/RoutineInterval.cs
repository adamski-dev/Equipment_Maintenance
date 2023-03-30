using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Models
{
    public class RoutineInterval
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Routine Interval")]
        [Required]
        [MaxLength(15)]
        public string Interval { get; set; }
    }
}
