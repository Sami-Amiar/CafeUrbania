using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeUrbania.Models.Validateurs
{
    public class ValiderQteMinCaractères : ValidationAttribute
    {
        int _iQteCaracteres;
        string _sErrorMessagep;

        public ValiderQteMinCaractères(int iQteCaractères, string sErrorMessage) : base(sErrorMessage)
        {
            this._iQteCaracteres = iQteCaractères;
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            // Obtenir la longueur de la propriete
            string? propertyValue = ((string?)value);

            // Vérifier la longueur de la propriete
            if ((propertyValue.Trim().Length) >= _iQteCaracteres)
            {
                return ValidationResult.Success;
            }
            else
            {
                var errorMessage = FormatErrorMessage(_iQteCaracteres.ToString());
                return new ValidationResult(errorMessage, (IEnumerable<string>)new[] { validationContext.DisplayName });
            }
        }
    }
}
