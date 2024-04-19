using CafeUrbania.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CaféUrbania.Test
{
    public class ContactezNous_PrenomUnitTest
    {
        readonly WebApplicationFactory<Program> _application;
        readonly HttpClient? _client;

        public ContactezNous_PrenomUnitTest()
        {
            // Arrange
            _application = new WebApplicationFactory<Program>();
            _client = _application.CreateClient();
        }

        [Fact]
        public async Task Test1Async()
        {
            // Act
            var response = await _client.GetStringAsync("/Bonjour");

            // Assert
            Assert.Equal("Hello World!", response);
        }

        [Fact]
        public async Task Test001_CreateContact_PrenomInvalideValeurVide()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("Merci de saisir votre prénom", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test002_CreateContact_PrenomValideValeurNull()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = null,
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("Merci de saisir votre prénom", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test003_CreateContact_PrenomInvalideAvec2Caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "La",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("Veuillez entrer plus de 3 caractères", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test004_CreateContact_PrenomValideAvec3Caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Lau",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            var temp = await result.Content.ReadAsStringAsync();
            Assert.Contains("Lau", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test005_CreateContact_PrenomAvecPremiereLettreMajuscule()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "abc",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

            var formattedResponse = result.Content.ReadAsStringAsync().Result;
            Contact infosObtenues = JsonConvert.DeserializeObject<Contact>(formattedResponse);
            Assert.Contains("Abc", infosObtenues.Prenom);
        }

        [Fact]
        public async Task Test006_CreateContact_PrenomInvalide3Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "   ",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("Merci de saisir votre prénom", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test007_CreateContact_PrenomInvalide2Espaces1Caractere()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "  a",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("Veuillez entrer plus de 3 caractères", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test008_CreateContact_PrenomInvalide1Caractere2Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "a  ",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("Veuillez entrer plus de 3 caractères", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test009_CreateContact_PrenomInvalide2Espaces1Caractere2Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "  a  ",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("Veuillez entrer plus de 3 caractères", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test010_CreateContact_PrenomValide2Espaces3Caracteres2Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "  abc  ",
                Nom = "Testeur",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("  abc  ", await result.Content.ReadAsStringAsync());
        }
    }
}