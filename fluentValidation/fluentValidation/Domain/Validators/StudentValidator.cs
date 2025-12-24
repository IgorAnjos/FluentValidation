using fluentValidation.Domain.Entities;
using FluentValidation;
using System.Linq;

namespace fluentValidation.Domain.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Nome é obrigatório")
                .Must(name => name.IsValid)
                .WithMessage(student => FormatNameErrors(student.Name));

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email é obrigatório")
                .Must(email => email.IsValid)
                .WithMessage(student => FormatEmailErrors(student.Email));

            RuleFor(x => x.Document)
                .NotNull()
                .WithMessage("CPF é obrigatório")
                .Must(document => document.IsValid)
                .WithMessage(student => FormatDocumentErrors(student.Document));
        }

        private string FormatNameErrors(ValueObjects.Name name)
        {
            return name?.Errors.Any() == true
                ? $"Nome inválido: {string.Join(", ", name.Errors)}"
                : "Nome inválido";
        }

        private string FormatEmailErrors(ValueObjects.Email email)
        {
            return email?.Errors.Any() == true
                ? $"Email inválido: {string.Join(", ", email.Errors)}"
                : "Email inválido";
        }

        private string FormatDocumentErrors(ValueObjects.Document document)
        {
            return document?.Errors.Any() == true
                ? $"CPF inválido: {string.Join(", ", document.Errors)}"
                : "CPF inválido";
        }
    }
}
