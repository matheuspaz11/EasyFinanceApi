using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EasyFinanceApi.Helpers.Validators
{
    public class StringValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string texto = value.ToString();

                if (!Regex.IsMatch(texto, @"^[a-zA-Z]+$"))
                {
                    return new ValidationResult("O campo não pode conter caracteres especiais ou números.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
