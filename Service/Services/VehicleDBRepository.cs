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

        public IDbSet<IVehicleModel> Models { get; set; }
        public IDbSet<IVehicleMake> Makes { get; set; }
    }
}