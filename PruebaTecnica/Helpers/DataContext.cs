namespace PruebaTecnica.Helpers
{
    using PruebaTecnica.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    public class DataContext : DbContext
    {
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(Configuration.GetConnectionString("pruebaTecnica"));
            }
        }
    }
}
