using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Service.Interfaces;
using Service.Models;

namespace Service.Services
{
    public class VehicleDBContext : DbContext
    {

        public VehicleDBContext() : base("VehicleDB")
        {
        }

        public DbSet<VehicleMake> Makers { get; set; }
        public DbSet<VehicleModel> Models { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}