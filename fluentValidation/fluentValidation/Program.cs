using fluentValidation.Domain.Entities;
using fluentValidation.Domain.ValueObjects;
using System;

namespace fluentValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== DEMONSTRAÇÃO FLUENTVALIDATION ===\n");

            // Exemplo 1: Nome inválido
            Console.WriteLine("1. Testando Nome inválido:");
            var invalidName = new Name("", "");
            Console.WriteLine($"   Nome válido? {invalidName.IsValid}");
            if (!invalidName.IsValid)
            {
                foreach (var error in invalidName.Errors)
                    Console.WriteLine($"   - {error}");
            }

            // Exemplo 2: Nome válido
            Console.WriteLine("\n2. Testando Nome válido:");
            var validName = new Name("João", "Silva");
            Console.WriteLine($"   Nome: {validName}");
            Console.WriteLine($"   Nome válido? {validName.IsValid}");

            // Exemplo 3: Email inválido
            Console.WriteLine("\n3. Testando Email inválido:");
            var invalidEmail = new Email("teste");
            Console.WriteLine($"   Email válido? {invalidEmail.IsValid}");
            if (!invalidEmail.IsValid)
            {
                foreach (var error in invalidEmail.Errors)
                    Console.WriteLine($"   - {error}");
            }

            // Exemplo 4: Email válido
            Console.WriteLine("\n4. Testando Email válido:");
            var validEmail = new Email("joao.silva@exemplo.com.br");
            Console.WriteLine($"   Email: {validEmail}");
            Console.WriteLine($"   Email válido? {validEmail.IsValid}");

            // Exemplo 5: CPF inválido
            Console.WriteLine("\n5. Testando CPF inválido:");
            var invalidDocument = new Document("12345678900");
            Console.WriteLine($"   CPF válido? {invalidDocument.IsValid}");
            if (!invalidDocument.IsValid)
            {
                foreach (var error in invalidDocument.Errors)
                    Console.WriteLine($"   - {error}");
            }

            // Exemplo 6: CPF válido
            Console.WriteLine("\n6. Testando CPF válido:");
            var validDocument = new Document("12345678909"); // CPF válido para teste
            Console.WriteLine($"   CPF: {validDocument}");
            Console.WriteLine($"   CPF válido? {validDocument.IsValid}");

            // Exemplo 7: Student inválido
            Console.WriteLine("\n7. Testando Student com dados inválidos:");
            var invalidStudent = new Student(
                new Name("", ""),
                new Document("123"),
                new Email("email-invalido")
            );
            Console.WriteLine($"   Student válido? {invalidStudent.IsValid}");
            if (!invalidStudent.IsValid)
            {
                foreach (var error in invalidStudent.Errors)
                    Console.WriteLine($"   - {error}");
            }

            // Exemplo 8: Student válido
            Console.WriteLine("\n8. Testando Student com dados válidos:");
            var validStudent = new Student(
                new Name("Maria", "Santos"),
                new Document("12345678909"),
                new Email("maria.santos@exemplo.com")
            );
            Console.WriteLine($"   Nome: {validStudent.Name}");
            Console.WriteLine($"   CPF: {validStudent.Document}");
            Console.WriteLine($"   Email: {validStudent.Email}");
            Console.WriteLine($"   Student válido? {validStudent.IsValid}");

            // Exemplo 9: Validação detalhada com ValidationResult
            Console.WriteLine("\n9. Validação detalhada de Student:");
            var studentForDetailedValidation = new Student(
                new Name("A", ""), // Nome muito curto
                new Document("11111111111"), // CPF com dígitos repetidos
                new Email("email@invalido") // Email sem TLD válido
            );
            var validationResult = studentForDetailedValidation.ValidateWithDetails();
            Console.WriteLine($"   É válido? {validationResult.IsValid}");
            if (!validationResult.IsValid)
            {
                Console.WriteLine("   Erros encontrados:");
                foreach (var failure in validationResult.Errors)
                {
                    Console.WriteLine($"   - Propriedade: {failure.PropertyName}");
                    Console.WriteLine($"     Erro: {failure.ErrorMessage}");
                    Console.WriteLine($"     Tentativa de valor: {failure.AttemptedValue}");
                }
            }

            Console.WriteLine("\n=== FIM DA DEMONSTRAÇÃO ===");
            Console.ReadKey();
        }
    }
}
