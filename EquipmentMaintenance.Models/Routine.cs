using EquipmentMaintenance.Utility;
using Microsoft.AspNetCore.Mvc;
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
    public class Routine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Routine Description")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Routine Description must be between 3 and 30 characters in length.")]
        public string RoutineDescription { get; set; }

        [Display(Name = "Item")]
        public int EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        [ValidateNever]
        public Equipment Equipment { get; set; }

        [Display(Name = "Primary Resource")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Range(1, 31)]
        public int Frequency { get; set; }

        [Required]
        [Display(Name = "Interval")]
        public int IntervalId { get; set; }

        [ForeignKey("IntervalId")]
        [ValidateNever]
        public RoutineInterval RoutineInterval { get; set; }

        public bool IsActiveStatus { get; set; } = true;

        [Display(Name = "Date Last Completed")]
        public DateTime? DateLastCompleted { get; set; }

        [Display(Name = "Next Due Date")]
        [DateRangeValidation(ErrorMessage = "Next Due Date must be equal to or greater than today's date")]
        public DateTime NextDueDate { get; set; }

        [Required]
        [Display(Name = "Days To Overdue")]
        [Range(1, 30)]
        public int DaysToOverdue { get; set; }

        [Required]
        public IEnumerable<Tasks> ListOfTasks { get; set; }

    }
}
