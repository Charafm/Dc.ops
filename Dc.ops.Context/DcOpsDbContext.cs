using Dc.ops.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dc.Ops.Context
{
    public class DcOpsDbContext : DbContext
    {
        public DcOpsDbContext(DbContextOptions<DcOpsDbContext> options) : base(options) { }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<EquipmentHistory> EquipmentHistories { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<AssignLocation> AssignLocations { get; set; }
        public DbSet<AssignHistory> AssignHistories { get; set; }

        
    }
}
