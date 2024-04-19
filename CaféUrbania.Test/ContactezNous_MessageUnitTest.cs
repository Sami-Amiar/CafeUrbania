using CafeUrbania.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CaféUrbania.Test
{
    public class ContactezNous_MessageUnitTest
    {
        readonly WebApplicationFactory<Program> _application;
        readonly HttpClient? _client;

        public ContactezNous_MessageUnitTest()
        {
            // arrange
            _application = new WebApplicationFactory<Program>();
            _client = _application.CreateClient();
        }

        [Fact]
        public async Task Test055_CreateContact_MessageInvalideValeurVide()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = ""
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("N'oubliez pas de décrire le plus clairement possible le problème rencontré.", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test056_CreateContact_MessageInvalideAvec10Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "          "
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("N'oubliez pas de décrire le plus clairement possible le problème rencontré.", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test057_CreateContact_MessageInvalideAvec4caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "YOLO"
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("La description du message doit comporter au moins 5 caractères.", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test058_CreateContact_MessageInvalideAvec3Espaces3Caracteres3Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "   LOL   "
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("La description du message doit comporter au moins 5 caractères.", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test059_CreateContact_MessageValideAvec5Caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwert"
            });

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.Contains("qwert", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test060_CreateContact_MessageValideAvec3Espaces5Caracteres3Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "   qwert   "
            });

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            Assert.Contains("   qwert   ", await result.Content.ReadAsStringAsync());
        }
    }
}
