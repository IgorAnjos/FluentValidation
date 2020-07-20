using System;

namespace fluentValidation.Domain.ValueObjects
{
    public class Email
    {
        public Email(string address)
        {
            Address = address;

            if (Address.Length < 5)
                throw new Exception("E-mail inválido");
        }
        public string Address { get; private set; }
        public override string ToString()
        {
            return $"{Address}";
        }
    }
}
