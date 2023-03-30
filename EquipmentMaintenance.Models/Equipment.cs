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
    public class Equipment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 30 character in length.")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Identifier")]
        [RegularExpression(@"\W*(ABC)\d{4}", ErrorMessage = "Use ABC prefix followed by 4 digit number.")]
        public string UniqueIdentifier { get; set; }
        [Required]
        [Display(Name = "Model Number")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Model Number must be between 3 and 30 character in length.")]
        public string ModelNumber { get; set; }
        [Required]
        [Display(Name = "Serial Number")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Serial Number must be between 3 and 30 character in length.")]
        public string SerialNumber { get; set; }
        [Display(Name = "In Service Status")]
        public bool InServiceStatus { get; set; } = true;
        [Display(Name = "Date Created")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Equipment Type")]
        public int EquipmentTypeId { get; set; }

        [ForeignKey("EquipmentTypeId")]
        [ValidateNever]
        public EquipmentType EquipmentType { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [ForeignKey("LocationId")]
        [ValidateNever]
        public Location Location { get; set; }
    }
}
