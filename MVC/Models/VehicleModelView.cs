
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Models
{
    
    public class VehicleModelView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public Guid VehicleMakeId { get; set; }
        public IEnumerable<VehicleMakeView> MakeEnumerable { get; set; }
        public VehicleMakeView Make { get; set; }
    }
}