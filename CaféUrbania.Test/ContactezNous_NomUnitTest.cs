using CafeUrbania.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace CaféUrbania.Test
{
    public class ContactezNous_NomUnitTest
    {
        readonly WebApplicationFactory<Program> _application;
        readonly HttpClient? _client;

        public ContactezNous_NomUnitTest()
        {
            // Arrange
            _application = new WebApplicationFactory<Program>();
            _client = _application.CreateClient();
        }

        [Fact]
        public async Task Test020_CreateContact_NomInvalideValeurVide()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("Merci de saisir votre nom de famille", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test021_CreateContact_NomValideValeurNull()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = null,
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("Merci de saisir votre nom de famille", await result.Content.ReadAsStringAsync());
        }



        [Fact]
        public async Task Test022_CreateContact_NomInvalideAvec51Caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = string.Concat(Enumerable.Repeat("X", 51)),
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("Veuillez entrer moins de 50 caractères", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test023_CreateContact_NomValideAvec50Caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = string.Concat(Enumerable.Repeat("X", 50)),
                CategorieDemande = 1,
                Message = "qwerty"
            });
            var formattedResponse = result.Content.ReadAsStringAsync().Result;
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            //Assert.Contains(string.Concat(Enumerable.Repeat("X", 50)), await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test024_CreateContact_NomInvalide30Espaces51Caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "                              " + string.Concat(Enumerable.Repeat("X", 51)),
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Contains("Veuillez entrer moins de 50 caractères", await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Test025_CreateContact_NomAvecToutesLesLettresMajuscules()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "abc",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

            var formattedResponse = result.Content.ReadAsStringAsync().Result;
            Contact infosObtenues = JsonConvert.DeserializeObject<Contact>(formattedResponse);
            Assert.Contains("ABC", infosObtenues.Nom);
        }

        [Fact]
        public async Task Test026_CreateContact_NomValide50Caracteres10Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "          " + string.Concat(Enumerable.Repeat("X", 50)),
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task Test027_CreateContact_NomValide10Espaces50Caracteres()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = string.Concat(Enumerable.Repeat("X", 50)) + "          ",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            var temp = await result.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task Test028_CreateContact_NomValide10Espaces50Caracteres10Espaces()
        {
            // Act
            var result = await _client.PostAsJsonAsync("/contact", new Contact
            {
                Prenom = "Testeur",
                Nom = "          " + string.Concat(Enumerable.Repeat("X", 50)) + "          ",
                CategorieDemande = 1,
                Message = "qwerty"
            });

            // Assert
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }
    }
}
