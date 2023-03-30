using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
    {
        private ApplicationDbContext _db;

        public WorkOrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(WorkOrder obj)
        {
            _db.WorkOrders.Update(obj);
        }

    }
}
