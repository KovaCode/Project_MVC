using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MVC_Project.Models;

namespace MVC_Project.DAL
{
    public class VehicleDBContext : DbContext
    {
        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }
    }
}