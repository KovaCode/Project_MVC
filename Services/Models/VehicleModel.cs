using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class VechicleModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int MakeID { get; set; }
        [Required]       
        public string Name { get; set; }
        public string Abrv { get; set; }

        public virtual VechicleMake Makers { get; set; }
    }
}