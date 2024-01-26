namespace PruebaTecnica.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using PruebaTecnica.Entities;
    using PruebaTecnica.Helpers;
    using PruebaTecnica.Models;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using BC = BCrypt.Net.BCrypt;

    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public class UsuarioPersonaController : BaseController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DbSet<Persona> Persona { get; set; }

        public UsuarioPersonaController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("autenticar")]
        public ActionResult<AutenticarResponse> Autenticar(AutenticarRequest model)
        {
            var usuario = _context.Usuario.SingleOrDefault(x => x.NombreUsuario == model.Usuario);

            if (usuario == null || !BC.Verify(model.Contrasena, usuario.Contrasena))
                throw new Exception("El correo electrónico o la contraseña suministrada no son correctos");

            var jwtToken = generarJwtToken(usuario);

            _context.Update(usuario);
            _context.SaveChanges();

            var response = _mapper.Map<AutenticarResponse>(usuario);

            response.JwtToken = jwtToken;

            return Ok(response);
        }

        [HttpPost("persona")]
        public IActionResult CreatePersona([FromBody] Persona persona)
        {
            if (ModelState.IsValid)
            {
                _context.Persona.Add(persona);
                _context.SaveChanges();
                return Ok(new { success = true, message = "Persona se ha registrado con exito" });
            }

            return BadRequest("Debe enviar todos los campos.");
        }

        [HttpPost("usuario")]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(usuario.Contrasena))
                {
                    ModelState.AddModelError(nameof(usuario.Contrasena), "La contraseña es requerida.");
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(usuario.NombreUsuario))
                {
                    ModelState.AddModelError(nameof(usuario.NombreUsuario), "Usuario es requerido");
                    return BadRequest(ModelState);
                }
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
                return Ok(new { success = true, message = "Usuario se ha registrado con exito" });
            }

            return BadRequest("Debe enviar todos los campos.");
        }

        [HttpGet("personas")]
        public IActionResult GetPersonas()
        {
            var personas = _context.Persona.FromSqlRaw("EXEC GetPersonas").ToList();

            return Ok(personas);
        }

        private string generarJwtToken(Usuario usuario)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("8ed23b3d-9112-4060-beed-dbd04d9098bb");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", usuario.Id.ToString()) }),
                    Expires = DateTime.Now.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generando el JWT: {ex}");
                throw;
            }
        }
    }
}
