
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC.Models
{
    
    public class VehicleModelView
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please enter Model Name.")]
        [StringLength(50, ErrorMessage = "The Model Name must be less than {1} characters.")]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public Guid VehicleMakeId { get; set; }
        public IEnumerable<VehicleMakeView> MakeEnumerable { get; set; }
        public VehicleMakeView Make { get; set; }
    }
}