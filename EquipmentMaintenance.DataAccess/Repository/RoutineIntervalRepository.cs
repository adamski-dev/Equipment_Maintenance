using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class RoutineIntervalRepository : Repository<RoutineInterval>, IRoutineIntervalRepository
    {
        private ApplicationDbContext _db;

        public RoutineIntervalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(RoutineInterval obj)
        {
            _db.Intervals.Update(obj);
        }
    }
}
