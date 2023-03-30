using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private ApplicationDbContext _db;

        public LocationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Location obj)
        {
            _db.Locations.Update(obj);
        }
    }
}
