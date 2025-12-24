# FluentValidation - Projeto de Demonstração

## 📚 Sobre o Projeto

Este projeto demonstra o uso avançado da biblioteca **FluentValidation** no .NET, implementando validações robustas em Value Objects e Entidades de Domain-Driven Design (DDD).

## 🎯 Conceitos Implementados

### Domain Notifications vs Exceptions

Este projeto segue o padrão de **Domain Notifications** em vez de lançar exceções para validações. Isso proporciona:

- ✅ Melhor controle de fluxo da aplicação
- ✅ Coleta de múltiplos erros de uma vez
- ✅ Código mais limpo e testável
- ✅ Mensagens de erro mais detalhadas

## 🚀 Recursos do FluentValidation Utilizados

### 1. **Validadores Personalizados**
- **NameValidator**: Validação de nomes com regex, comprimento e formato
- **EmailValidator**: Validação de email com formato e estrutura
- **DocumentValidator**: Validação completa de CPF com algoritmo real
- **StudentValidator**: Validação de entidade composta

### 2. **Regras Avançadas Implementadas**

#### NameValidator
```csharp
- NotEmpty(): Campo obrigatório
- MinimumLength(2): Mínimo 2 caracteres
- MaximumLength(100): Máximo 100 caracteres
- Matches(): Apenas letras e espaços (com acentuação)
```

#### EmailValidator
```csharp
- NotEmpty(): Campo obrigatório
- EmailAddress(): Formato de email válido
- MaximumLength(254): Limite padrão de email
- Must(): Validação customizada de formato
```

#### DocumentValidator
```csharp
- NotEmpty(): Campo obrigatório
- Length(11): Exatamente 11 dígitos
- Must(BeOnlyNumbers): Apenas números
- Must(BeAValidCPF): Algoritmo completo de validação de CPF
```

#### StudentValidator
```csharp
- NotNull(): Objetos obrigatórios
- Must(IsValid): Validação de Value Objects aninhados
- Mensagens personalizadas com detalhes dos erros
```

## 📁 Estrutura do Projeto

```
fluentValidation/
├── Domain/
│   ├── Entities/
│   │   └── Student.cs           # Entidade com validação
│   ├── ValueObjects/
│   │   ├── Name.cs               # Value Object para Nome
│   │   ├── Email.cs              # Value Object para Email
│   │   └── Document.cs           # Value Object para CPF
│   └── Validators/
│       ├── NameValidator.cs      # Validador de Nome
│       ├── EmailValidator.cs     # Validador de Email
│       ├── DocumentValidator.cs  # Validador de CPF
│       └── StudentValidator.cs   # Validador de Entidade
└── Program.cs                    # Exemplos de uso
```

## 💻 Como Usar

### 1. Instalar Dependências

```bash
dotnet restore
```

### 2. Executar o Projeto

```bash
dotnet run
```

## 🔍 Exemplos de Uso

### Validação de Value Objects

```csharp
// Nome inválido
var invalidName = new Name("", "");
if (!invalidName.IsValid)
{
    foreach (var error in invalidName.Errors)
        Console.WriteLine(error);
}

// Nome válido
var validName = new Name("João", "Silva");
Console.WriteLine($"Nome: {validName}"); // João Silva
```

### Validação de Email

```csharp
// Email inválido
var invalidEmail = new Email("teste");
Console.WriteLine($"Válido? {invalidEmail.IsValid}"); // False

// Email válido
var validEmail = new Email("joao@exemplo.com");
Console.WriteLine($"Válido? {validEmail.IsValid}"); // True
```

### Validação de CPF

```csharp
// CPF inválido
var invalidCPF = new Document("12345678900");
if (!invalidCPF.IsValid)
{
    foreach (var error in invalidCPF.Errors)
        Console.WriteLine(error);
}

// CPF válido
var validCPF = new Document("12345678909");
Console.WriteLine($"Válido? {validCPF.IsValid}"); // True
```

### Validação de Entidade

```csharp
var student = new Student(
    new Name("Maria", "Santos"),
    new Document("12345678909"),
    new Email("maria@exemplo.com")
);

if (student.IsValid)
{
    // Processar estudante
}
else
{
    foreach (var error in student.Errors)
        Console.WriteLine(error);
}
```

### Validação Detalhada

```csharp
var student = new Student(...);
var validationResult = student.ValidateWithDetails();

if (!validationResult.IsValid)
{
    foreach (var failure in validationResult.Errors)
    {
        Console.WriteLine($"Propriedade: {failure.PropertyName}");
        Console.WriteLine($"Erro: {failure.ErrorMessage}");
        Console.WriteLine($"Valor tentado: {failure.AttemptedValue}");
    }
}
```

## 🎓 Recursos do FluentValidation Demonstrados

### Validadores Básicos
- ✅ `NotEmpty()` / `NotNull()`
- ✅ `MinimumLength()` / `MaximumLength()`
- ✅ `Length()`
- ✅ `EmailAddress()`
- ✅ `Matches()` com Regex

### Validadores Customizados
- ✅ `Must()` com funções personalizadas
- ✅ Algoritmo de validação de CPF
- ✅ Validação de estrutura de email

### Mensagens Personalizadas
- ✅ `WithMessage()` estático
- ✅ `WithMessage()` dinâmico com contexto
- ✅ Formatação de mensagens compostas

### Validação de Objetos Complexos
- ✅ Validação de propriedades aninhadas
- ✅ Agregação de erros de múltiplos níveis
- ✅ `ValidationResult` detalhado

## 🛠️ Tecnologias

- **.NET 8.0**
- **FluentValidation 11.9.0**
- **C# 12** com Nullable Reference Types

## 📝 Padrões Implementados

1. **Value Objects**: Objetos imutáveis com validação interna
2. **Domain Notifications**: Coleta de erros sem exceções
3. **Single Responsibility**: Validadores separados por responsabilidade
4. **Fail Fast**: Validação no construtor
5. **Composition**: Entidades validam seus Value Objects

## 🧪 Casos de Teste Demonstrados

O `Program.cs` contém 9 cenários de teste:

1. Nome inválido (vazio)
2. Nome válido
3. Email inválido (formato)
4. Email válido
5. CPF inválido (algoritmo)
6. CPF válido
7. Student com dados inválidos
8. Student com dados válidos
9. Validação detalhada com ValidationResult

## 📖 Referências

- [FluentValidation Documentation](https://docs.fluentvalidation.net/)
- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [Value Objects Pattern](https://martinfowler.com/bliki/ValueObject.html)

## 🎯 Próximos Passos

Para expandir este projeto, considere:

1. **Adicionar mais validadores**:
   - Telefone (com DDD e formato brasileiro)
   - CEP (com formato e validação)
   - Data de nascimento (idade mínima/máxima)

2. **Implementar testes unitários**:
   - Usar xUnit ou NUnit
   - Testar todos os cenários de validação

3. **Integrar com ASP.NET Core**:
   - Validação automática em Controllers
   - FluentValidation.AspNetCore

4. **Adicionar localização**:
   - Mensagens em múltiplos idiomas
   - ResourceManager

5. **Implementar validação assíncrona**:
   - Validação de unicidade em banco de dados
   - Chamadas a APIs externas

## 📄 Licença

Este é um projeto de demonstração educacional.
