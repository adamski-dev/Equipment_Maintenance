using EquipmentMaintenance.DataAccess.Repository.IRepository;
using EquipmentMaintenance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentMaintenance.DataAccess.Repository
{
    public class TasksRepository : Repository<Tasks>, ITasksRepository
    {
        private ApplicationDbContext _db;

        public TasksRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Tasks obj)
        {
            _db.Tasks.Update(obj);
        }
    }
}
