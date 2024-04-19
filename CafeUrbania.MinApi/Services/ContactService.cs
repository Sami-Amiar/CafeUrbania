using CafeUrbania.MinApi.Services.Interfaces;
using CafeUrbania.Models;

namespace CafeUrbania.MinApi.Services;

public class ContactService : IContactService
{
    public Contact Create(Contact contact)
    {
        // S'assurer que l'objet existe
        if (contact != null)
        {
            // S'assurer que la valeur n'est pas nulle, autrement ToUpper avorte
            if (!string.IsNullOrWhiteSpace(contact.Prenom))
            {
                // Return char and concat substring
                contact.Prenom = char.ToUpper(contact.Prenom[0]) + contact.Prenom.Substring(1);
            }

            if (!string.IsNullOrWhiteSpace(contact.Nom))
            {
                // Return char and concat substring
                contact.Nom = contact.Nom.ToUpper();
            }

            if (!string.IsNullOrWhiteSpace(contact.Courriel))
            {
                contact.Courriel = contact.Courriel.ToLower();
            }

            if (!string.IsNullOrWhiteSpace(contact.CourrielConfirmation))
            {
                contact.CourrielConfirmation = contact.CourrielConfirmation.ToLower();
            }
        }

        // Retourner l'objet
        return contact;
    }

    public List<Categories> GetCategory()
    {
        return new List<Categories>()
        {
            new Categories { CategorieId = 1, CategorieDescription = "Suggestions" },
            new Categories { CategorieId = 2, CategorieDescription = "Problème avec ma commande" },
            new Categories { CategorieId = 3, CategorieDescription = "Plaintes" },
            new Categories { CategorieId = 4, CategorieDescription = "Autres" },
        };
    }

}
