using fluentValidation.Domain.Validators;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace fluentValidation.Domain.ValueObjects
{
    public class Name
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            Validate();
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; private set; } = new List<string>();

        private void Validate()
        {
            var firstNameValidator = new NameValidator("Nome");
            var lastNameValidator = new NameValidator("Sobrenome");

            var firstNameResult = firstNameValidator.Validate(FirstName ?? string.Empty);
            var lastNameResult = lastNameValidator.Validate(LastName ?? string.Empty);

            AddErrors(firstNameResult);
            AddErrors(lastNameResult);
        }

        private void AddErrors(ValidationResult result)
        {
            if (!result.IsValid)
            {
                Errors.AddRange(result.Errors.Select(e => e.ErrorMessage));
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
