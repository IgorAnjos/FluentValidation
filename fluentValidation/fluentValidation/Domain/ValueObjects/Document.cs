using System;
using System.Collections.Generic;
using System.Text;

namespace fluentValidation.Domain.ValueObjects
{
    public class Document
    {
        public Document(string number)
        {
            Number = number;

            if (Number.Length != 11)
                throw new Exception("CPF inválido");
        }
        public string Number { get; private set; }

        public override string ToString()
        {
            return Number;
        }
    }
}
