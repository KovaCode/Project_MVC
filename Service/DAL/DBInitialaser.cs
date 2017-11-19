using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Service.Models;

namespace Service.DAL
{
    public class DBInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<VehicleDBContext>
    {
        protected override void Seed(VehicleDBContext context)
        {
            var students = new List<VehicleMake>
            {
            new VehicleMake{Name="Audi", Abrv="Test abrv. 1."},
            new VehicleMake{Name="BMW", Abrv="Test abrv. 1."},
            new VehicleMake{Name="Chevroled", Abrv="Test abrv. 1."},
            new VehicleMake{Name="Daihatsu", Abrv="Test abrv. 1."},
            new VehicleMake{Name="VW", Abrv="Test abrv. 1."},
            new VehicleMake{Name="Enso"},
            new VehicleMake{Name="Fiat", Abrv="Test abrv. 1."},
            new VehicleMake{Name="G", Abrv="Test abrv. 1."},
            new VehicleMake{Name="Honda", Abrv="Test abrv. 1."},
            new VehicleMake{Name="Tesla", Abrv="Test abrv. 1."},
 
            };

            students.ForEach(s => context.Makers.Add(s));
            context.SaveChanges();
 
        }
    }
}