using CafeUrbania.Models;
using CafeUrbania.Models.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CafeUrbania.UI.Components;

public partial class ContactForm
{
    [Inject]
    public IContactService ContactService_ { get; set; }
    public Contact Contact { get; set; } = new Contact();

    public bool HasContacted = false;

    List<Categories> Categories = new List<Categories>();

    // Desactiver par defaut le telephone à l'ouverture du formulaire
    public bool telephoneDisabled = true;
    // Desactiver par defaut le courriel à l'ouverture du formulaire
    public bool courrielDisabled = true;
    // Desactiver par defaut la confirmation du courriel à l'ouverture du formulaire
    public bool courrielConfirmationDisabled = true;


    // Pour la gestion du modèle comme les erreurs de validation
    private EditContext editContext;
    protected override async Task OnInitializedAsync()
    {
        Categories = (await ContactService_.GetCategory());

        Contact.ChoixNotification = 1;
        Contact.DateHeureCreation = System.DateTime.Now;

        // Associer le contexte au modèle
        editContext = new EditContext(Contact);
    }

    private async void HandleValidSubmit()
    {
        await ContactService_.PostContact(Contact);
        HasContacted = true;
        Contact = new();
    }

    /// <summary>
    /// Permet d'appliquer le choix de l'utilisateur sur les differentes zone du formulaire
    /// </summary>
    public void OnChangeChoixNotification()
    {
        if (Contact.ChoixNotification == 1)
        {
            telephoneDisabled = true;
            courrielDisabled = true;
            courrielConfirmationDisabled = true;
            Contact.Courriel = null;
            Contact.Telephone = null;
        }

        else
        {
            if (Contact.ChoixNotification == 3)
            {
                telephoneDisabled = false;
                Contact.Courriel = null;
            }

            else
            {
                telephoneDisabled = true;
            }

            if (Contact.ChoixNotification == 2)
            {
                courrielDisabled = false;
                telephoneDisabled = true;
                Contact.Telephone = null;
            }

            else
            {
                courrielDisabled = true;
            }
        }

        // Afficher le message de la valeur requise
        // À améliorer : l'afficher éventuellement seulement si tente de sauvegarder
        // Vérifier null car les tests plantes autrement car null
        if (editContext != null)
        {
            editContext.Validate();
        }
    }
}
