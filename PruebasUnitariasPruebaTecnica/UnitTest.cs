using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using PruebaTecnica.Controllers;
using PruebaTecnica.Entities;
using PruebaTecnica.Helpers;
using PruebaTecnica.Models;
using Xunit;

namespace PruebasUnitariasPruebaTecnica
{
    public class UnitTest
    {
        private readonly UsuarioPersonaController _controller;
        private readonly DataContext _context;

        public UnitTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(options, new ConfigurationBuilder().Build());

            SeedTestData();

            _controller = new UsuarioPersonaController(_context, mapper);
        }

        [Fact]
        public void AutenticarRetornaOk()
        {
            var validCredentials = new AutenticarRequest { Usuario = "test", Contrasena = "12345678" };
            var result = _controller.Autenticar(validCredentials);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<AutenticarResponse>(okResult.Value);
            Assert.NotNull(response.JwtToken);
        }

        [Fact]
        public void AutenticarArrojaException()
        {
            var invalidCredentials = new AutenticarRequest { Usuario = "invalidUser", Contrasena = "invalidPassword" };

            Assert.Throws<Exception>(() => _controller.Autenticar(invalidCredentials));
        }

        [Fact]
        public void CreatePersonaRetornaOk()
        {
            var validPersona = new Persona { Nombres = "test", Apellidos = "test", Email = "test@test.com", NumeroIdentificacion = "1234567890", TipoIdentificacion = "test" };

            var result = _controller.CreatePersona(validPersona);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var jsonResponse = JsonConvert.SerializeObject(okResult.Value);
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
            Assert.True((bool)response["success"]);
            Assert.Equal("Persona se ha registrado con exito", response["message"]);
        }

        [Fact]
        public void CreatePersonaBadRequest()
        {
            var invalidPersona = new Persona { Nombres = "test", Apellidos = "test", Email = "test@test.com" }; 

            var result = _controller.CreatePersona(invalidPersona);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void CreateUsuarioRetornaOk()
        {
            var validUsuario = new Usuario { NombreUsuario = "test", Contrasena = "12345678" };

            var result = _controller.CreateUsuario(validUsuario);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var jsonResponse = JsonConvert.SerializeObject(okResult.Value);
            var response = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
            Assert.True((bool)response["success"]);
            Assert.Equal("Usuario se ha registrado con exito", response["message"]);
        }

        [Fact]
        public void CreateUsuarioBadRequest()
        {
            var invalidUsuario = new Usuario { NombreUsuario = "test1" };

            var result = _controller.CreateUsuario(invalidUsuario);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetPersonasRetornaOk()
        {
            var result = _controller.GetPersonas();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var personas = Assert.IsType<List<Persona>>(okResult.Value);
            Assert.NotEmpty(personas);
        }

        private void SeedTestData()
        {
            var personas = new List<Persona>
        {
            new Persona { Nombres = "test", Apellidos = "test", Email = "test@test.com", NumeroIdentificacion = "1234567890", TipoIdentificacion = "test" },
        };

            _context.Persona.AddRange(personas);
            _context.SaveChanges();
        }
    }
}