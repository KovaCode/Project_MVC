using Service.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models.Entity
{
<<<<<<< HEAD:Service/Models/VehicleModel.cs
    public class VehicleModel
=======
    public class VehicleModelEntity
>>>>>>> StaticPagging:Service/Models/Entity/VehicleModelEntity.cs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [ForeignKey("Make")]
        public Guid VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual VehicleMakeEntity Make { get; set; }
    }
}