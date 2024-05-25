using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Estilo
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Ingresa el Nombre del Estilo")]
        [Display(Name ="Nombre Estilo")]
        public string Nombre { get; set; }
    }
}
