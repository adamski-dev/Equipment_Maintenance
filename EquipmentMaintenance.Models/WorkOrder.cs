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
    public class WorkOrder
    {
        [Key]
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("W/O Number")]
        [Required]
        public string WorkOrderNumber { get; set; }
        public int? WorkOrderRoutineId { get; set; }

        [Display(Name = "Item")]
        [Required]
        public int EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        [ValidateNever]
        public Equipment Equipment { get; set; }

        [Display(Name = "Job Description")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 30 character in length.")]
        public string JobDescription { get; set; }

        [Display(Name = "Primary Resource")]
        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DateDue { get; set; }
        
        [Display(Name = "Overdue Date")]
        public DateTime? DateOverdue { get; set; }

        [Display(Name = "Created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool CompletedFlag { get; set; } = false;
        public IEnumerable<Tasks>? ListOfTasks { get; set; }
        public IEnumerable<Routine>? RoutineList { get; set; }
        
        [Required]
        public int WeekNumber { get; set; }
        public string? Comment { get; set; }
        public bool UnplannedFlag { get; set; } = false;

        [Display(Name = "Date Completed")]
        public DateTime? DateCompleted { get; set; }

    }
}
