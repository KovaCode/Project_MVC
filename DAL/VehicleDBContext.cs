using System;
using System.Data.Entity;
using DAL.Entity;
using Model.Common;

namespace DAL
{
    public class VehicleDBContext : DbContext, IVehicleDBContext
    {

        public VehicleDBContext() : base("VehicleDB")
        {
        }

        public DbSet<VehicleMakeEntity> Makers { get; set; }
        public DbSet<VehicleModelEntity> Models { get; set; }
    }
}