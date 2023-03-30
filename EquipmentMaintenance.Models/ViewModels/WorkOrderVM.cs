using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Models.ViewModels
{
    public class WorkOrderVM
    {
        public WorkOrder WorkOrder { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem> ApplicationUsers { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem> Equipment { get; set; }
        
        [ValidateNever]
        public IEnumerable<Tasks> TasksList { get; set; }

    }

}
