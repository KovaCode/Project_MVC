
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Models
{
    
    public class VehicleModelView
    {
        public Guid Id { get; set; }
        public Guid MakeID { get; set; }
        public string MakeName { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}