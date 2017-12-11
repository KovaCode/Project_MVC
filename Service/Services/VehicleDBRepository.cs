using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Service.Interfaces;
using Service.Models.Entity;

namespace Service.Services
{
    public class VehicleDBContext : DbContext
    {

        public VehicleDBContext() : base("VehicleDB")
        {
        }

        public DbSet<VehicleMakeEntity> Makers { get; set; }
        public DbSet<VehicleModelEntity> Models { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}