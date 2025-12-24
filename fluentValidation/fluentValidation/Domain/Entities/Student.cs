using fluentValidation.Domain.ValueObjects;
using fluentValidation.Domain.Validators;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace fluentValidation.Domain.Entities
{
    public class Student
    {
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;

            Validate();
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; private set; } = new List<string>();

        private void Validate()
        {
            var validator = new StudentValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                Errors.AddRange(result.Errors.Select(e => e.ErrorMessage));
            }
        }

        public ValidationResult ValidateWithDetails()
        {
            var validator = new StudentValidator();
            return validator.Validate(this);
        }
    }
}
