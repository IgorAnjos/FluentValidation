using FluentValidation;
using System.Linq;

namespace fluentValidation.Domain.Validators
{
    public class DocumentValidator : AbstractValidator<string>
    {
        public DocumentValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("CPF não pode ser vazio")
                .Length(11)
                .WithMessage("CPF deve ter 11 dígitos")
                .Must(BeOnlyNumbers)
                .WithMessage("CPF deve conter apenas números")
                .Must(BeAValidCPF)
                .WithMessage("CPF inválido");
        }

        private bool BeOnlyNumbers(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            return cpf.All(char.IsDigit);
        }

        private bool BeAValidCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.Distinct().Count() == 1)
                return false;

            // Calcula o primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += int.Parse(cpf[i].ToString()) * (10 - i);

            int remainder = sum % 11;
            int digit1 = remainder < 2 ? 0 : 11 - remainder;

            if (int.Parse(cpf[9].ToString()) != digit1)
                return false;

            // Calcula o segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += int.Parse(cpf[i].ToString()) * (11 - i);

            remainder = sum % 11;
            int digit2 = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cpf[10].ToString()) == digit2;
        }
    }
}
