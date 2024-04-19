using CafeUrbania.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CaféUrbania.Test;

public class ContactezNous_TelephoneUnitTest
{
    readonly WebApplicationFactory<Program> _application;
    readonly HttpClient? _client;

    public ContactezNous_TelephoneUnitTest()
    {
        _application = new WebApplicationFactory<Program>();
        _client = _application.CreateClient();
    }

    [Fact]
    public async Task Test039_CreateContact_AucuneNotificationTelephoneNonRequis()
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
    public async Task Test040_CreateContact_NotificationTelephoneTelephoneNullRequis()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 3,
            Telephone = null,
            Message = "qwerty"
        });

        // Assert
        Assert.Contains("Merci de saisir votre numéro de téléphone", await result.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Test041_CreateContact_NotificationTelephoneTelephoneValide10Caracteres()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 3,
            Telephone = "1234567890",
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task Test047_CreateContact_NotificationTelephoneTelephoneFormatInvalide9Chiffres()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 3,
            Telephone = "123456789",
            Message = "qwerty"
        });

        // Assert
        Assert.Contains("Veuillez entrer exactement 10 chiffres.", await result.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Test048_CreateContact_NotificationTelephoneTelephoneFormatInvalide11Chiffres()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 3,
            Telephone = "12345678901",
            Message = "qwerty"
        });

        // Assert
        Assert.Contains("Veuillez entrer exactement 10 chiffres.", await result.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Test049_CreateContact_NotificationTelephoneTelephoneFormatValide10Chiffres()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 3,
            Telephone = "1234567890",
            Message = "qwerty"
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);
    }

    [Fact]
    public async Task Test050_CreateContact_NotificationTelephoneTelephoneFormatInvalide1Lettre9Chiffres()
    {
        // Act
        var result = await _client.PostAsJsonAsync("/contact", new Contact
        {
            Prenom = "Testeur",
            Nom = "Testeur",
            CategorieDemande = 0,
            ChoixNotification = 3,
            Telephone = "a123456789",
            Message = "qwerty"
        });

        // Assert
        Assert.Contains("Veuillez entrer exactement 10 chiffres.", await result.Content.ReadAsStringAsync());
    }
}
