using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Service.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Service.DAL
{
    public class VehicleDBContext : DbContext
    {

        public VehicleDBContext() : base("VehicleDatabase")
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