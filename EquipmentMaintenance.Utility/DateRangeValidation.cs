using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.Utility
{
    public class DateRangeValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime date = Convert.ToDateTime(value);
            return DateOnly.FromDateTime(date) >= DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
