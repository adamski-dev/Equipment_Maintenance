using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEquipmentTypeRepository EquipmentType { get; }
        ILocationRepository Location { get; }
        IEquipmentRepository Equipment { get; }
        IRoutineRepository Routine { get; }
        ITasksRepository Tasks { get; }
        IRoutineIntervalRepository Interval { get; }
        IWorkOrderRepository WorkOrder { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
