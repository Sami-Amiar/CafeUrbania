using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeUrbania.Models.Validateurs
{
    internal class ValiderQteMaxCaractères : ValidationAttribute
    {
        int _iQteCaracteres;
        string _sErrorMessagep;

        public ValiderQteMaxCaractères(int iQteCaractères, string sErrorMessage) : base(sErrorMessage)
        {
            this._iQteCaracteres = iQteCaractères;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            // Obtenir la longueur du nom
            string? nom = ((CafeUrbania.Models.Contact)validationContext.ObjectInstance).Nom;

            // Vérifier la longueur du nom
            if (nom.Trim().Length <= _iQteCaracteres)
            {
                return ValidationResult.Success;
            }
            else
            {
                var errorMessage = FormatErrorMessage(_iQteCaracteres.ToString());
                return new ValidationResult(errorMessage, (IEnumerable<string>)new[] { "nom" });
            }
        }
    }
}