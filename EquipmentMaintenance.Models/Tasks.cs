using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(400, MinimumLength = 5)]
        public string? Description { get; set; }

        public int RoutineId { get; set; }

        [ForeignKey("RoutineId")]
        [ValidateNever]
        public Routine Routine { get; set; }

    }
}
