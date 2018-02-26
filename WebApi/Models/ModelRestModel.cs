
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    
    public class ModelRestModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please enter Model Name.")]
        [StringLength(50, ErrorMessage = "The Model Name must be less than {1} characters.")]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public Guid VehicleMakeId { get; set; }
        public IEnumerable<MakeRestModel> MakeEnumerable { get; set; }
        public MakeRestModel Make { get; set; }
    }
}