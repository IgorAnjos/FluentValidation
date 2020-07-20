using fluentValidation.Domain.ValueObjects;

namespace fluentValidation.Domain.Entities
{
    public class Student
    {
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
    }
}
