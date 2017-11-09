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

        public VehicleDBContext() : base("name=VehicleDBContext")
            {
            }

        public DbSet<Maker> Makers { get; set; }
        public DbSet<Model> Models { get; set; }





    }
}