using Model.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity
{
    public class VehicleModelEntity : IVehicleModelModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Make")]
        public Guid VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        //public virtual VehicleMakeEntity Make { get; set; }
        IVehicleMakeModel IVehicleModelModel.Make { get; set; }
    }
}