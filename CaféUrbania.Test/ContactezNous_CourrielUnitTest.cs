using CafeUrbania.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CaféUrbania.Test;

public class ContactezNous_CourrielUnitTest
{
    readonly WebApplicationFactory<Program> _application;
    readonly HttpClient? _client;

    public ContactezNous_CourrielUnitTest()
    {
        _application = new WebApplicationFactory<Program>();
        _client = _application.CreateClient();
    }

    [Fact]
    public async Task Test042_CreateContact_AucuneNotificationCourrielNonRequis()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 1,
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task Test043_CreateContact_NotificationCourrielCourrielNullRequis()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 2,
            Courriel = null,
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Contains("Merci d'entrer votre adresse courriel", await result.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Test044_CreateContact_NotificationCourrielCourrielValideFormatValide()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 2,
            Courriel = "a@a.aa",
            CourrielConfirmation = "a@a.aa",
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task Test051_CreateContact_NotificationCourrielCourrielFormatInvalide()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 2,
            Courriel = "a.a",
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Contains("S.V.P. entrer les informations dans un format valide.", await result.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Test052_CreateContact_NotificationCourrielCourrielEtCourrielConfirmationIdentiques()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 2,
            Courriel = "a@a.aa", 
            CourrielConfirmation = "a@a.aa",
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task Test053_CreateContact_NotificationCourrielCourrielEtCourrielConfirmationDifferents()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 2,
            Courriel = "a@a.aa",
            CourrielConfirmation = "b@b.bb",
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Contains("Le courriel de confirmation n'est pas le même que le courriel. Le corriger.", await result.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Test054_CreateContact_NotificationCourrielCourrielLettresMinuscules()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 2,
            Courriel = "A@A.AA",
            CourrielConfirmation = "A@A.AA",
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        var formattedResponseCourriel = result.Content.ReadAsStringAsync().Result;
        Contact infosObtenuesCourriel = JsonConvert.DeserializeObject<Contact>(formattedResponseCourriel);
        Assert.Contains("a@a.aa", infosObtenuesCourriel.Courriel);

        var formattedResponseConfirmation = result.Content.ReadAsStringAsync().Result;
        Contact infosObtenuesConfirmation = JsonConvert.DeserializeObject<Contact>(formattedResponseConfirmation);
        Assert.Contains("a@a.aa", infosObtenuesConfirmation.CourrielConfirmation);
    }
}
