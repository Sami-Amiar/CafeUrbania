using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeUrbania.Models.Validateurs
{
    internal class RequiredIfTelephone : ValidationAttribute
    {
        public RequiredIfTelephone(string sErrorMessage) : base(sErrorMessage)
        {

        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var contact = (Contact)validationContext.ObjectInstance;

            if (contact.ChoixNotification == 3)
            {
                // Check if telephone number is provided
                if (string.IsNullOrEmpty(((CafeUrbania.Models.Contact)validationContext.ObjectInstance).Telephone))
                {
                    var errorMessage = FormatErrorMessage(ErrorMessage);
                    return new ValidationResult(errorMessage, new[] { "Telephone" });
                }
            }

            return ValidationResult.Success;
        }
    }
}
