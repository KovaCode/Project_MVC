using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class VehicleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string VehicleMakeId { get; set; }
        [Required]       
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}