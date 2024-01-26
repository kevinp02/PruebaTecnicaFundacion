using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Models
{
    public class AutenticarRequest
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        public string Contrasena { get; set; }
    }
}
