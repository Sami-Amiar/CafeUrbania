using CafeUrbania.Models;
using CafeUrbania.Test.Fixtures;
using CafeUrbania.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace CaféUrbania.Test;

public class ContactezNous_NotificationsUnitTest : TestBed<FormContactFixture>
{
    public ContactezNous_NotificationsUnitTest(ITestOutputHelper testOutputHelper, FormContactFixture fixture) : base(testOutputHelper, fixture)
    {
    }

    [Fact]
    public async Task Test031_FormulaireContact_AucuneNotificationZonesTelephoneEtCourrielsDesactivees()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 1;

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(true, formulaire.telephoneDisabled);
        Assert.Equal(true, formulaire.courrielDisabled);
        Assert.Equal(true, formulaire.courrielConfirmationDisabled);
    }

    [Fact]
    public async Task Test032_FormulaireContact_NotificationTelephone_ZoneTelephoneActivee()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 3;

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(false, formulaire.telephoneDisabled);
    }

    [Fact]
    public async Task Test033_FormulaireContact_NotificationTelephone_ZonesCourrielsDesactivees()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 3;

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(true, formulaire.courrielDisabled);
    }

    [Fact]
    public async Task Test034_FormulaireContact_NotificationTelephone_ZonesCourrielsValeursNulles()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 3;

        formulaire.Contact.Courriel = "sdgseg";
        
        formulaire.OnChangeChoixNotification();
        
        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(null, formulaire.Contact.Courriel);
    }

    [Fact]
    public async Task Test035_FormulaireContact_NotificationCourriel_ZonesCourrielsActivees()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 2;

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(false, formulaire.courrielDisabled);
    }

    [Fact]
    public async Task Test036_FormulaireContact_NotificationCourriel_ZoneTelephoneDesactive()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 2;

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(true, formulaire.telephoneDisabled);
    }

    [Fact]
    public async Task Test037_FormulaireContact_NotificationCourriel_ZoneTelephoneValeurNulle()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 2;

        formulaire.Contact.Telephone = "sdgseg";

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(null, formulaire.Contact.Telephone);
    }

    [Fact]
    public async Task Test038_FormulaireContact_AucuneNotification_ZonesTelephoneEtCourrielsValeursNulles()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 1;

        formulaire.Contact.Courriel = "sdgseg";
        formulaire.Contact.Telephone = "sdgseg";

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(null, formulaire.Contact.Courriel);
        Assert.Equal(null, formulaire.Contact.Telephone);
    }

    [Fact]
    public async Task Test039_FormulaireContact_AucuneNotification_ZonesTelephoneEtCourrielsDesactivees()
    {
        // Arrange
        ContactForm formulaire = new ContactForm();
        // Act
        // Ne pas me notifier  = 1, Courriel = 2, Téléphone = 3
        formulaire.Contact.ChoixNotification = 3;

        formulaire.OnChangeChoixNotification();

        // Assert
        // Résulat PRÉVU, résultat OBTENU
        Assert.Equal(false, formulaire.telephoneDisabled);
    }
}
