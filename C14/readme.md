# C# 14 - Novidades e Diferenças em Relação às Versões Anteriores

> **Lançamento:** Novembro 2025 | **Visual Studio 2026** | **.NET 10**

## Documentação Oficial

- [What's new in C# 14 - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)
- [Introducing C# 14 - .NET Blog](https://devblogs.microsoft.com/dotnet/introducing-csharp-14/)
- [Histórico do C# - Microsoft Learn](https://learn.microsoft.com/pt-br/dotnet/csharp/whats-new/csharp-version-history)

---

## Índice

1. [Visão Geral: C# 14 vs Versões Anteriores](#visão-geral-c-14-vs-versões-anteriores)
2. [Recursos por Categoria](#recursos-por-categoria)
3. [Detalhamento de Cada Recurso](#detalhamento-de-cada-recurso)
   - [Extension Members](#extension-members)
   - [The field Keyword](#the-field-keyword)
   - [Null-Conditional Assignment](#null-conditional-assignment)
   - [Unbound Generic Types and nameof](#unbound-generic-types-and-nameof)
   - [Simple Lambda Parameters with Modifiers](#simple-lambda-parameters-with-modifiers)
   - [Partial Events and Constructors](#partial-events-and-constructors)
   - [User-Defined Compound Assignment](#user-defined-compound-assignment)
   - [Implicit Span Conversions](#implicit-span-conversions)
   - [Preprocessor Directives for File-based Apps](#preprocessor-directives-for-file-based-apps)

---

## Visão Geral: C# 14 vs Versões Anteriores

O C# 14 traz **Extension Members** como destaque principal, além de recursos que reduzem boilerplate e melhoram performance. Requer **.NET 10**.

| Antes (C# 13 e anteriores) | Depois (C# 14) |
|-----------------------------|----------------|
| Apenas extension methods | Extension properties, operators e static members |
| Campo backing explícito para lógica em um accessor | `field` keyword para propriedades com lógica parcial |
| `if (x != null) x.Prop = value;` | `x?.Prop = value;` |
| `nameof(List<int>)` para obter "List" | `nameof(List<>)` — tipo genérico não vinculado |
| `(string text, out int result) => ...` com tipos obrigatórios | `(text, out result) => ...` — modificadores sem tipo |
| Eventos e construtores não podiam ser partial | Partial events e partial constructors |
| `sum = sum.Add(v)` em loops | `sum += v` com operador definido pelo usuário |
| `line.AsSpan(0, 5)` ou `new Span(buffer, 0, 8)` | Conversões implícitas: `line[..5]`, `buffer[..8]` |

---

## Recursos por Categoria

### Destaque Principal
- **Extension Members** – Propriedades, operadores e membros estáticos como extensões

### Produtividade
- **field keyword** – Propriedades com lógica em um accessor sem campo explícito
- **Null-conditional assignment** – `?.` e `?[]` no lado esquerdo de atribuições
- **nameof com tipos genéricos não vinculados** – `nameof(List<>)`
- **Modificadores em parâmetros de lambda** – `out`, `ref`, `in`, `scoped` sem tipo
- **Partial events e constructors** – Mais membros partial para source generators

### Performance
- **User-defined compound assignment** – Operadores `+=`, `-=`, etc. definidos pelo usuário
- **Implicit span conversions** – Conversões entre `Span`, `ReadOnlySpan` e arrays

### Outros
- **Preprocessor directives for file-based apps** – Novas diretivas para apps baseados em arquivo

---

## Detalhamento de Cada Recurso

### Extension Members

**Antes (C# 13):** Apenas extension methods. Propriedades e operadores não podiam ser extensões.

```csharp
// Apenas métodos
public static class EnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> source) => !source.Any();
}
```

**Depois (C# 14):** Extension blocks com propriedades, operadores e membros estáticos.

```csharp
public static class EnumerableExtensions
{
    // Membros de instância
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !source.Any();
        public IEnumerable<TSource> Where(Func<TSource, bool> predicate) { ... }
    }

    // Membros estáticos (sem receiver name)
    extension<TSource>(IEnumerable<TSource>)
    {
        public static IEnumerable<TSource> Identity => Enumerable.Empty<TSource>();
        public static IEnumerable<TSource> operator +(IEnumerable<TSource> left, IEnumerable<TSource> right)
            => left.Concat(right);
    }
}

// Uso
int[] data = [1, 2, 3];
if (data.IsEmpty) { ... }
var combined = data + [4, 5];
var empty = IEnumerable<int>.Identity;
```

**Compatibilidade:** Source e binary compatible com extension methods existentes. Migração gradual possível.

**Referências:**
- [Extension members - Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods)
- [extension keyword - Microsoft Docs](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/extension)

---

### The field Keyword

**Antes (C# 13):** Lógica em um accessor exigia campo backing explícito e ambos os accessors.

```csharp
private string _message = "";
public string Message
{
    get => _message;
    set => _message = value ?? throw new ArgumentNullException(nameof(value));
}
```

**Depois (C# 14):** O token `field` referencia o campo sintetizado pelo compilador.

```csharp
public string Message
{
    get;
    set => field = value ?? throw new ArgumentNullException(nameof(value));
}
```

Permite manter auto-property e adicionar lógica apenas onde necessário. Útil para validação, normalização ou guards em um único accessor.

**Nota:** Se o tipo tiver um símbolo chamado `field`, use `@field` ou `this.field` para desambiguar.

---

### Null-Conditional Assignment

**Antes (C# 13):** Verificação explícita antes de atribuir.

```csharp
if (customer is not null)
{
    customer.Order = GetCurrentOrder();
    customer.Total += CalculateIncrement();
}
```

**Depois (C# 14):** `?.` e `?[]` no lado esquerdo da atribuição.

```csharp
customer?.Order = GetCurrentOrder();
customer?.Total += CalculateIncrement();
```

O lado direito **só é avaliado** quando o receiver não é null. Operadores compostos (`+=`, `-=`, etc.) são suportados; `++` e `--` não.

---

### Unbound Generic Types and nameof

**Antes (C# 13):** `nameof` exigia tipo genérico fechado.

```csharp
var name = nameof(List<int>);  // "List" — precisava de um tipo arbitrário
```

**Depois (C# 14):** `nameof` aceita tipo genérico não vinculado.

```csharp
var name = nameof(List<>);  // "List"
```

Útil para logging, reflexão e mensagens de erro sem instanciar o tipo.

---

### Simple Lambda Parameters with Modifiers

**Antes (C# 13):** Modificadores como `out` exigiam tipos explícitos em todos os parâmetros.

```csharp
delegate bool TryParse<T>(string text, out T result);
TryParse<int> parse = (string text, out int result) => int.TryParse(text, out result);
```

**Depois (C# 14):** Modificadores permitidos com parâmetros implicitamente tipados.

```csharp
TryParse<int> parse = (text, out result) => int.TryParse(text, out result);
```

Modificadores suportados: `scoped`, `ref`, `in`, `out`, `ref readonly`. O modificador `params` ainda exige tipos explícitos.

---

### Partial Events and Constructors

**Antes (C# 13):** Eventos e construtores não podiam ser partial.

**Depois (C# 14):** Suporte a partial events e partial constructors.

```csharp
public partial class Widget(int size, string name)
{
    public partial event EventHandler Changed;
}

public partial class Widget
{
    public partial event EventHandler Changed
    {
        add => _changed += value;
        remove => _changed -= value;
    }
    private EventHandler? _changed;

    public Widget()
    {
        Initialize();
    }
}
```

Facilita source generators e separação de responsabilidades em tipos grandes. O partial constructor deve ter exatamente uma declaração defining e uma implementing.

---

### User-Defined Compound Assignment

**Antes (C# 13):** Operadores compostos (`+=`, `-=`) eram derivados do operador binário. Sem suporte explícito para atualização in-place.

```csharp
BigVector sum = BigVector.Zero;
foreach (var v in values)
{
    sum = sum.Add(v);  // Cria intermediário a cada iteração
}
```

**Depois (C# 14):** Operador compound assignment definido explicitamente.

```csharp
public struct BigVector(float x, float y, float z)
{
    public static BigVector operator +(BigVector l, BigVector r) => ...;
    public void operator +=(BigVector r)
    {
        X += r.X;
        Y += r.Y;
        Z += r.Z;
    }
}

// Uso
foreach (var v in values)
{
    sum += v;  // Chama operator += diretamente
}
```

Melhora performance em loops com tipos numéricos e vetoriais (ex.: SIMD).

---

### Implicit Span Conversions

**Antes (C# 13):** Conversões explícitas para `Span` e `ReadOnlySpan`.

```csharp
string line = ReadLine();
ReadOnlySpan<char> key = line.AsSpan(0, 5);
ProcessKey(key);

int[] buffer = GetBuffer();
Span<int> head = new(buffer, 0, 8);
Accumulate(head);
```

**Depois (C# 14):** Conversões implícitas entre `Span`, `ReadOnlySpan` e arrays.

```csharp
string line = ReadLine();
ProcessKey(line[..5]);  // Slice converte implicitamente

int[] buffer = GetBuffer();
Accumulate(buffer[..8]);
```

Menos código e melhor suporte a otimizações no runtime. Usado nas bibliotecas do .NET 10 para parsing e processamento de texto.

---

### Preprocessor Directives for File-based Apps

Novas diretivas de preprocessador para aplicações baseadas em arquivo. Consulte a [documentação de preprocessor directives](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/preprocessor-directives#file-based-apps).

---

## Breaking Changes

O C# 14 introduz algumas breaking changes. Consulte:
- [Breaking changes - C# 14 / .NET 10](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/breaking-changes/compiler%20breaking%20changes%20-%20dotnet%2010)

Exemplo conhecido: [Enumerable.Reverse](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/breaking-changes/compiler%20breaking%20changes%20-%20dotnet%2010#enumerablereverse).

---

## Requisitos

- **.NET 10 SDK**
- **Visual Studio 2026** (ou versão mais recente)

---

## Referências Adicionais

- [What's new in .NET 10](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview)
- [C# language versioning](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/configure-language-version)
- [Extension members specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-14.0/extensions)
- [Null-conditional assignment specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-14.0/null-conditional-assignment)
- [User-defined compound assignment specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-14.0/user-defined-compound-assignment)
- [First-class span types specification](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-14.0/first-class-span-types)
