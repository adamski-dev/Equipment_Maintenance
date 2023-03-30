using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository.IRepository
{
    public interface IEquipmentTypeRepository : IRepository<EquipmentType>
    {
        void Update(EquipmentType obj);
    }
}
