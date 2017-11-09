using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
    
    public class Model
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int MakeID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual Maker Makers { get; set; }
    }
}