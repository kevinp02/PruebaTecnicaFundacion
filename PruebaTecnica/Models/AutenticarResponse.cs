namespace PruebaTecnica.Models
{
    public class AutenticarResponse
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string JwtToken { get; set; }
    }
}
