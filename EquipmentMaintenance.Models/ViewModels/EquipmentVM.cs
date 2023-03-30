using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Models.ViewModels
{
    public class EquipmentVM
    {
        public Equipment Equipment { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> EquipmentTypeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LocationList { get; set; }

    }
}
