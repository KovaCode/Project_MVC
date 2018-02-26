using System;
using System.Data.Entity;
using DAL.Entity;

namespace DAL
{
    public interface IVehicleDBContext
    {
        DbSet<VehicleMakeEntity> Makers { get; set; }
        DbSet<VehicleModelEntity> Models { get; set; }
    }
}