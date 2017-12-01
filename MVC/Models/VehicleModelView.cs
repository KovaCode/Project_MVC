
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Models
{
    
    public class VehicleModelView
    {
        public Guid Id { get; set; }
        public string VehicleMakeId { get; set; }
        public string VehicleMakeName { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}