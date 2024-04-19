using CafeUrbania.Models.Validateurs;
using System.ComponentModel.DataAnnotations;

namespace CafeUrbania.Models;

public class Contact
{
    [Required(ErrorMessage = "Merci de saisir votre prénom")]
    //[MinLengthAttribute(3, ErrorMessage = "Veuillez entrer plus de caractères")]
    [ValiderQteMinCaractères(3, "Veuillez entrer plus de {0} caractères")]
    public string Prenom { get; set; }

    [Required(ErrorMessage = "Merci de saisir votre nom de famille")]
    [ValiderQteMaxCaractères(50, "Veuillez entrer moins de {0} caractères")]
    public string Nom { get; set; }

    // Indiquer ? même si Required pour faire afficher le message d'erreur dans le formulaire si vide 
    [Required(ErrorMessage = "Merci de sélectionner une catégorie de demande.")]
    public int? CategorieDemande { get; set; }

    // Pour retenir le choix de l'utilisateur des boutons radios sur le choix de notification
    public int ChoixNotification { get; set; }

    [RegularExpression("^[0-9]{10}$", ErrorMessage = "Veuillez entrer exactement 10 chiffres.")]
    [RequiredIfTelephone("Merci de saisir votre numéro de téléphone")]
    public string Telephone { get; set; }

    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "S.V.P. entrer les informations dans un format valide.")]
    [RequiredIfCourriel("Merci d'entrer votre adresse courriel")]
    public string Courriel { get; set; }

    [Compare("Courriel", ErrorMessage = "Le courriel de confirmation n'est pas le même que le courriel. Le corriger.")]
    public string? CourrielConfirmation { get; set; }

    [Required(ErrorMessage = "N'oubliez pas de décrire le plus clairement possible le problème rencontré.")]
    [ValiderQteMinCaractères(5, "La description du message doit comporter au moins 5 caractères.")]
    public string Message { get; set; }

    public DateTime DateHeureCreation { get; set; }
}
