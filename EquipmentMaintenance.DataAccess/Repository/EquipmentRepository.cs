using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        private ApplicationDbContext _db;

        public EquipmentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Equipment obj)
        {
            // used conditional update of items in case of not wanting to include all items during update

            var objFromDb = _db.Equipment.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)   
            {
                objFromDb.Description = obj.Description;
                objFromDb.UniqueIdentifier = obj.UniqueIdentifier;
                objFromDb.ModelNumber = obj.ModelNumber;
                objFromDb.SerialNumber = obj.SerialNumber;
                objFromDb.InServiceStatus = obj.InServiceStatus;
            }
        }
    }
}
