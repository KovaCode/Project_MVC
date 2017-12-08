using Service.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class VehicleModel : IVehicleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Make")]
        public Guid VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual VehicleMake Make { get; set; }
    }
}