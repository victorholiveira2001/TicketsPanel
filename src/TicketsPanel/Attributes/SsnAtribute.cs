using System.ComponentModel.DataAnnotations;
using TicketsPanel.Services;

namespace TicketsPanel.Attributes
{
    public class SsnAtribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("O campo CPF é obrigatório.");
            }


            var ssnvalidator = new SsnValidator();

            if (!ssnvalidator.Validate(value.ToString()))
            {
                return new ValidationResult(ErrorMessage ?? "O CPF informado é inválido.");
            }


            return ValidationResult.Success;
        }
    }
}
