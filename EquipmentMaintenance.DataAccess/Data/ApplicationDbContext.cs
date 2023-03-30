using EquipmentMaintenance.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquipmentMaintenance.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<EquipmentType> EquipmentType { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<RoutineInterval> Intervals { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
    }
}
