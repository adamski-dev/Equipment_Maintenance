using EquipmentMaintenance.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            EquipmentType = new EquipmentTypeRepository(_db);
            Location = new LocationRepository(_db);
            Equipment = new EquipmentRepository(_db);
            Routine = new RoutineRepository(_db);
            Tasks = new TasksRepository(_db);
            Interval = new RoutineIntervalRepository(_db);
            WorkOrder = new WorkOrderRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }
        public IEquipmentTypeRepository EquipmentType { get; private set; }
        public ILocationRepository Location { get; private set; }
        public IEquipmentRepository Equipment { get; private set; }
        public IRoutineRepository Routine { get; private set; }
        public ITasksRepository Tasks { get; private set; }
        public IRoutineIntervalRepository Interval { get; private set; }
        public IWorkOrderRepository WorkOrder { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
