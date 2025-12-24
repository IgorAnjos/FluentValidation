using FluentValidation;

namespace fluentValidation.Domain.Validators
{
    public class EmailValidator : AbstractValidator<string>
    {
        public EmailValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("Email não pode ser vazio")
                .EmailAddress()
                .WithMessage("Email inválido")
                .MaximumLength(254)
                .WithMessage("Email deve ter no máximo 254 caracteres")
                .Must(BeAValidEmailFormat)
                .WithMessage("Formato de email inválido");
        }

        private bool BeAValidEmailFormat(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Validação adicional de formato
            var parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            return parts[0].Length > 0 && parts[1].Contains('.');
        }
    }
}
