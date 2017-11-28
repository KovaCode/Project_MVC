using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class VehicleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public int MakeID { get; set; }
        [Required]       
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VehicleMake Makers { get; set; }
    }
}