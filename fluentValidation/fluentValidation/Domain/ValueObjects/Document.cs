using fluentValidation.Domain.Validators;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace fluentValidation.Domain.ValueObjects
{
    public class Document
    {
        public Document(string number)
        {
            Number = number;
            Validate();
        }

        public string Number { get; private set; }
        public bool IsValid => !Errors.Any();
        public List<string> Errors { get; private set; } = new List<string>();

        private void Validate()
        {
            var validator = new DocumentValidator();
            var result = validator.Validate(Number ?? string.Empty);

            if (!result.IsValid)
            {
                Errors.AddRange(result.Errors.Select(e => e.ErrorMessage));
            }
        }

        public override string ToString()
        {
            return Number;
        }
    }
}
