using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class RoutineRepository : Repository<Routine>, IRoutineRepository
    {
        private ApplicationDbContext _db;

        public RoutineRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Routine obj)
        {
            _db.Routines.Update(obj);
        }
    }
}
