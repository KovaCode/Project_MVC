using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Service.Models;

namespace Service.DAL
{
    public class VehicleDBContext : DbContext
    {

        public VehicleDBContext()
            {
            }

        public DbSet<VechicleMake> Makers { get; set; }
        public DbSet<VechicleModel> Models { get; set; }
    }
}