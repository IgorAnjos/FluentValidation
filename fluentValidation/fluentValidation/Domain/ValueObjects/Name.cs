using FluentValidator;
using System;
using System.Collections.Generic;
using System.Text;

namespace fluentValidation.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if (FirstName.Length <= 0)
                AddNotification("FirstName", "Nome inválido");
                //throw new Exception("Nome inválido");

            if (LastName.Length <= 0)
                AddNotification("LastName", "Sobrenome inválido");
            //throw new Exception("Sobrenome inválido");
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
