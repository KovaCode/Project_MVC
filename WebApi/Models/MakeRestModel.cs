using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class MakeRestModel  
    { 
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please enter Make Name.")]
        [StringLength(50, ErrorMessage = "The Make Name must be less than {1} characters.")]
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}