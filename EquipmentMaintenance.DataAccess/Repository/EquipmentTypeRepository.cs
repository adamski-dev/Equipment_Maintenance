using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class EquipmentTypeRepository : Repository<EquipmentType>, IEquipmentTypeRepository
    {
        private ApplicationDbContext _db;

        public EquipmentTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(EquipmentType obj)
        {
            _db.EquipmentType.Update(obj);
        }
    }
}
