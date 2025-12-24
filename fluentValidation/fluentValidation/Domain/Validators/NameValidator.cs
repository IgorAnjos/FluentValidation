using FluentValidation;

namespace fluentValidation.Domain.Validators
{
    public class NameValidator : AbstractValidator<string>
    {
        public NameValidator(string propertyName)
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage($"{propertyName} não pode ser vazio")
                .MinimumLength(2)
                .WithMessage($"{propertyName} deve ter no mínimo 2 caracteres")
                .MaximumLength(100)
                .WithMessage($"{propertyName} deve ter no máximo 100 caracteres")
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$")
                .WithMessage($"{propertyName} deve conter apenas letras e espaços");
        }
    }
}
