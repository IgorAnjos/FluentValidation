using fluentValidation.Domain.Entities;
using fluentValidation.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace fluentValidation.Domain.Examples
{
    /// <summary>
    /// Exemplos avançados de uso do FluentValidation
    /// </summary>
    public class AdvancedExamples
    {
        /// <summary>
        /// Exemplo: Validação condicional
        /// Valida apenas se determinada condição for verdadeira
        /// </summary>
        public static void ConditionalValidationExample()
        {
            Console.WriteLine("=== VALIDAÇÃO CONDICIONAL ===");
            
            // Aqui você poderia criar um validador condicional
            // Por exemplo, validar CPF apenas para brasileiros
            
            var student = new Student(
                new Name("João", "Silva"),
                new Document("12345678909"),
                new Email("joao@exemplo.com")
            );
            
            Console.WriteLine($"Student válido: {student.IsValid}");
        }

        /// <summary>
        /// Exemplo: Validação em lote de múltiplos objetos
        /// </summary>
        public static void BatchValidationExample()
        {
            Console.WriteLine("\n=== VALIDAÇÃO EM LOTE ===");
            
            var students = new List<Student>
            {
                new Student(
                    new Name("João", "Silva"),
                    new Document("12345678909"),
                    new Email("joao@exemplo.com")
                ),
                new Student(
                    new Name("", ""),
                    new Document("123"),
                    new Email("email-invalido")
                ),
                new Student(
                    new Name("Maria", "Santos"),
                    new Document("12345678909"),
                    new Email("maria@exemplo.com")
                )
            };

            var validStudents = students.Where(s => s.IsValid).ToList();
            var invalidStudents = students.Where(s => !s.IsValid).ToList();

            Console.WriteLine($"Total de estudantes: {students.Count}");
            Console.WriteLine($"Válidos: {validStudents.Count}");
            Console.WriteLine($"Inválidos: {invalidStudents.Count}");

            if (invalidStudents.Any())
            {
                Console.WriteLine("\nEstudantes inválidos:");
                foreach (var student in invalidStudents)
                {
                    Console.WriteLine($"- {student.Name}");
                    foreach (var error in student.Errors)
                    {
                        Console.WriteLine($"  • {error}");
                    }
                }
            }
        }

        /// <summary>
        /// Exemplo: Agregação de erros de múltiplos níveis
        /// </summary>
        public static void ErrorAggregationExample()
        {
            Console.WriteLine("\n=== AGREGAÇÃO DE ERROS ===");
            
            var student = new Student(
                new Name("A", "B"), // Nomes muito curtos
                new Document("11111111111"), // CPF inválido
                new Email("email") // Email inválido
            );

            Console.WriteLine($"Student válido? {student.IsValid}");
            Console.WriteLine($"Total de erros: {GetTotalErrors(student)}");
            
            Console.WriteLine("\nErros do Student:");
            foreach (var error in student.Errors)
            {
                Console.WriteLine($"- {error}");
            }

            Console.WriteLine("\nErros detalhados por campo:");
            if (student.Name?.Errors.Any() == true)
            {
                Console.WriteLine("Nome:");
                foreach (var error in student.Name.Errors)
                    Console.WriteLine($"  • {error}");
            }
            
            if (student.Email?.Errors.Any() == true)
            {
                Console.WriteLine("Email:");
                foreach (var error in student.Email.Errors)
                    Console.WriteLine($"  • {error}");
            }
            
            if (student.Document?.Errors.Any() == true)
            {
                Console.WriteLine("CPF:");
                foreach (var error in student.Document.Errors)
                    Console.WriteLine($"  • {error}");
            }
        }

        /// <summary>
        /// Exemplo: Validação com diferentes cenários
        /// </summary>
        public static void DifferentScenariosExample()
        {
            Console.WriteLine("\n=== DIFERENTES CENÁRIOS ===");

            var scenarios = new[]
            {
                new { Name = "Cenário 1: Tudo vazio", FirstName = "", LastName = "", CPF = "", Email = "" },
                new { Name = "Cenário 2: Nome curto", FirstName = "A", LastName = "B", CPF = "12345678909", Email = "teste@exemplo.com" },
                new { Name = "Cenário 3: Nome com números", FirstName = "João123", LastName = "Silva456", CPF = "12345678909", Email = "teste@exemplo.com" },
                new { Name = "Cenário 4: Email sem @", FirstName = "João", LastName = "Silva", CPF = "12345678909", Email = "testeexemplo.com" },
                new { Name = "Cenário 5: CPF repetido", FirstName = "João", LastName = "Silva", CPF = "11111111111", Email = "teste@exemplo.com" },
                new { Name = "Cenário 6: Tudo válido", FirstName = "João", LastName = "Silva", CPF = "12345678909", Email = "teste@exemplo.com" }
            };

            foreach (var scenario in scenarios)
            {
                Console.WriteLine($"\n{scenario.Name}:");
                var student = new Student(
                    new Name(scenario.FirstName, scenario.LastName),
                    new Document(scenario.CPF),
                    new Email(scenario.Email)
                );

                Console.WriteLine($"  Válido? {student.IsValid}");
                if (!student.IsValid)
                {
                    Console.WriteLine($"  Erros ({student.Errors.Count}):");
                    foreach (var error in student.Errors.Take(3)) // Mostra apenas os 3 primeiros
                    {
                        Console.WriteLine($"    • {error}");
                    }
                    if (student.Errors.Count > 3)
                    {
                        Console.WriteLine($"    ... e mais {student.Errors.Count - 3} erro(s)");
                    }
                }
            }
        }

        /// <summary>
        /// Calcula o total de erros incluindo os erros dos Value Objects
        /// </summary>
        private static int GetTotalErrors(Student student)
        {
            int total = student.Errors.Count;
            
            if (student.Name?.Errors != null)
                total += student.Name.Errors.Count;
            
            if (student.Email?.Errors != null)
                total += student.Email.Errors.Count;
            
            if (student.Document?.Errors != null)
                total += student.Document.Errors.Count;

            return total;
        }

        /// <summary>
        /// Exemplo: Formatação de mensagens de erro para UI
        /// </summary>
        public static void ErrorFormattingForUIExample()
        {
            Console.WriteLine("\n=== FORMATAÇÃO PARA UI ===");
            
            var student = new Student(
                new Name("", "Silva"),
                new Document("123"),
                new Email("joao@")
            );

            if (!student.IsValid)
            {
                // Formato para exibir em uma API REST
                var apiResponse = new
                {
                    Success = false,
                    Errors = student.Errors.Select(e => new { Message = e }),
                    ValidationDetails = new
                    {
                        Name = student.Name?.Errors ?? new List<string>(),
                        Email = student.Email?.Errors ?? new List<string>(),
                        Document = student.Document?.Errors ?? new List<string>()
                    }
                };

                Console.WriteLine("Formato JSON para API:");
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(apiResponse, new System.Text.Json.JsonSerializerOptions 
                { 
                    WriteIndented = true 
                }));
            }
        }
    }
}
