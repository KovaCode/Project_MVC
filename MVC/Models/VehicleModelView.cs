
using Service.Models;
using System;

namespace MVC.Models
{
    
    public class VehicleModelView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VehicleMake Make { get; set; }
    }
}