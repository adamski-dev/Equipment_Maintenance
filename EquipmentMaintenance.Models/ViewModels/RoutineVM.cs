using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Models.ViewModels
{
    public class RoutineVM
    {
        public Routine Routine { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem> EquipmentList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ApplicationUsers { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Intervals { get; set; }
        
        [ValidateNever]
        public IEnumerable<Tasks> TasksList { get; set; }

    }

}
