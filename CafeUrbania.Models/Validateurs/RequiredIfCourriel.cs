using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeUrbania.Models.Validateurs
{
    internal class RequiredIfCourriel : ValidationAttribute
    {
        public RequiredIfCourriel(string sErrorMessage) : base(sErrorMessage)
        {

        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var contact = (Contact)validationContext.ObjectInstance;

            if (contact.ChoixNotification == 2)
            {
                // Check if courriel number is provided
                if (string.IsNullOrEmpty(((CafeUrbania.Models.Contact)validationContext.ObjectInstance).Courriel))
                {
                    var errorMessage = FormatErrorMessage(ErrorMessage);
                    return new ValidationResult(errorMessage, new[] { "Courriel" });
                }
            }

            return ValidationResult.Success;
        }
    }
}
