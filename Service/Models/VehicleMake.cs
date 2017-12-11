using Service.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Service.Models
{
    public class VehicleMake : IVehicleMake
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}