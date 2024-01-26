namespace PruebaTecnica.Helpers
{
    using AutoMapper;
    using PruebaTecnica.Entities;
    using PruebaTecnica.Models;
    
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, AutenticarResponse>();
        }
    }
}
