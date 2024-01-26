namespace PruebaTecnica.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Persona
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string TipoIdentificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [NotMapped]
        public string NumeroIdentificacionConTipo =>
            $"{TipoIdentificacion} - {NumeroIdentificacion}";

        [NotMapped]
        public string NombresApellidosConcatenados =>
            $"{Nombres} {Apellidos}";
    }
}
