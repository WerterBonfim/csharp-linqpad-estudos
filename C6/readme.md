# C# 6 - Novidades e Diferenças em Relação às Versões Anteriores

> **Lançamento:** Julho 2015 | **Visual Studio 2015** | **.NET Framework 4.6**

## Documentação Oficial

- [Histórico do C# - Microsoft Learn](https://learn.microsoft.com/pt-br/dotnet/csharp/whats-new/csharp-version-history)
- [New Features in C# 6 - Blog Microsoft](https://learn.microsoft.com/en-us/archive/blogs/csharpfaq/new-features-in-c-6)

---

## Índice

1. [Visão Geral: C# 6 vs C# 5](#visão-geral-c-6-vs-c-5)
2. [Recursos por Categoria](#recursos-por-categoria)
3. [Detalhamento de Cada Recurso](#detalhamento-de-cada-recurso)
   - [Using Static](#using-static)
   - [Auto-Property Initializers](#auto-property-initializers)
   - [Getter-Only Auto-Properties](#getter-only-auto-properties)
   - [Dictionary / Index Initializers](#dictionary--index-initializers)
   - [Nameof Expression](#nameof-expression)
   - [String Interpolation](#string-interpolation)
   - [Null-Conditional Operator](#null-conditional-operator)
   - [Expression-Bodied Members](#expression-bodied-members)
   - [Exception Filters](#exception-filters)
   - [Await in Catch and Finally](#await-in-catch-and-finally)
   - [Parameterless Constructors in Structs](#parameterless-constructors-in-structs)
   - [Extension Add em Collection Initializers](#extension-add-em-collection-initializers)

---

## Visão Geral: C# 6 vs C# 5

O C# 6 focou em **reduzir boilerplate** e tornar o código mais conciso, sem adicionar complexidade conceitual. A filosofia foi: *"melhorar cenários simples do dia a dia"*.

| Antes (C# 5) | Depois (C# 6) |
|--------------|---------------|
| `Console.WriteLine(Math.Sqrt(4));` | `WriteLine(Sqrt(4));` com `using static` |
| Construtor obrigatório para inicializar propriedades | `public string Nome { get; set; } = "Padrão";` |
| `String.Format("{0} tem {1} anos", nome, idade)` | `$"{nome} tem {idade} anos"` |
| `if (obj != null) obj.Metodo();` | `obj?.Metodo();` |
| `throw new ArgumentNullException("parametro");` | `throw new ArgumentNullException(nameof(parametro));` |
| Métodos verbosos com `return` e chaves | `int Soma(int a, int b) => a + b;` |
| `catch` + `if` + `throw` para filtrar exceções | `catch (Ex e) when (e.Codigo == 42)` |
| Sem `await` em `catch`/`finally` | `await` permitido em ambos |

---

## Recursos por Categoria

### Nível de Expressão
- **nameof** – strings verificadas em tempo de compilação
- **String interpolation** – formatação de strings com `$""`
- **Null-conditional operator** – acesso seguro a membros (`?.`, `?[]`)
- **Index initializers** – inicialização de dicionários com `[chave] = valor`

### Nível de Declaração
- **Auto-property initializers** – valor inicial na declaração
- **Getter-only auto-properties** – propriedades somente leitura
- **Expression-bodied members** – métodos/propriedades com `=>`

### Nível de Bloco
- **Exception filters** – `catch (Ex e) when (condição)`
- **Await in catch/finally** – operações assíncronas em tratamento de exceção

### Import e Outros
- **Using static** – importar membros estáticos
- **Parameterless constructors in structs** – construtores sem parâmetros em structs
- **Extension Add in collection initializers** – métodos `Add` de extensão reconhecidos

---

## Detalhamento de Cada Recurso

### Using Static

**Antes (C# 5):** Era necessário qualificar sempre com o nome da classe.

```csharp
Console.WriteLine(Math.Sqrt(4));
Console.WriteLine(Math.PI);
```

**Depois (C# 6):** Importe membros estáticos e use diretamente.

```csharp
using static System.Math;
using static System.Console;

WriteLine(Sqrt(4));
WriteLine(PI);
```

**Arquivo:** [C6-01-using-static.linq](C6-01-using-static.linq)

---

### Auto-Property Initializers

**Antes (C# 5):** Inicialização apenas no construtor.

```csharp
public class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Pessoa()
    {
        Nome = "Nome Padrão";
        Idade = 0;
    }
}
```

**Depois (C# 6):** Valor inicial na declaração da propriedade.

```csharp
public class Pessoa
{
    public string Nome { get; set; } = "Nome Padrão";
    public int Idade { get; set; } = 0;
}
```

**Arquivos:** [C6-auto-property-initializer.linq](C6-auto-property-initializer.linq)

---

### Getter-Only Auto-Properties

**Antes (C# 5):** Para propriedade somente leitura, era necessário campo backing e implementação manual.

```csharp
public class Pessoa
{
    private readonly string _nomeCompleto;
    public string NomeCompleto { get { return _nomeCompleto; } }

    public Pessoa(string nome, string sobrenome)
    {
        _nomeCompleto = nome + " " + sobrenome;
    }
}
```

**Depois (C# 6):** Propriedade somente leitura com inicializador ou atribuição no construtor.

```csharp
public class Pessoa
{
    public string NomeCompleto { get; } = "Jane Doe";

    // Ou atribuído no construtor:
    public string Nome { get; }
    public Pessoa(string nome, string sobrenome)
    {
        Nome = nome + " " + sobrenome;
    }
}
```

O campo backing é implicitamente `readonly`.

---

### Dictionary / Index Initializers

**Antes (C# 5):** Sintaxe com `{ chave, valor }`.

```csharp
var dict = new Dictionary<int, string>
{
    { 1, "um" },
    { 2, "dois" },
    { 3, "três" }
};
```

**Depois (C# 6):** Sintaxe com índice `[chave] = valor`, mais próxima do uso normal de dicionários.

```csharp
var dict = new Dictionary<int, string>
{
    [1] = "um",
    [2] = "dois",
    [3] = "três"
};
```

**Arquivo:** [C6-dictionary-initializer.linq](C6-dictionary-initializer.linq)

---

### Nameof Expression

**Antes (C# 5):** Strings manuais sujeitas a erros e refatoração quebrada.

```csharp
throw new ArgumentNullException("parametro");
// Se renomear "parametro", a string não é atualizada
```

**Depois (C# 6):** Expressão verificada em tempo de compilação.

```csharp
throw new ArgumentNullException(nameof(parametro));
// Refatoração atualiza automaticamente
```

A expressão `nameof` **não é avaliada em runtime** – o compilador substitui pelo nome. Útil para `INotifyPropertyChanged`, validação e exceções.

**Arquivo:** [C6-nameof-expression.linq](C6-nameof-expression.linq)

---

### String Interpolation

**Antes (C# 5):** `String.Format` com placeholders numerados.

```csharp
var s = String.Format("{0} tem {1} ano(s)", nome, idade);
```

**Depois (C# 6):** Expressões diretamente na string com `$""`.

```csharp
var s = $"{nome} tem {idade} ano(s)";
var formatado = $"{nome,-20} tem {idade:D3} anos";
```

Suporta alinhamento e formatadores como em `String.Format`.

**Arquivo:** [C6-string-interpolation.linq](C6-string-interpolation.linq)

---

### Null-Conditional Operator

**Antes (C# 5):** Verificações explícitas de null.

```csharp
int? length = null;
if (customers != null)
    length = customers.Length;

string cep = null;
if (pessoa != null && pessoa.Endereco != null)
    cep = pessoa.Endereco.CEP;
```

**Depois (C# 6):** Operadores `?.` e `?[]`.

```csharp
int? length = customers?.Length;
string cep = pessoa?.Endereco?.CEP;
var primeiro = customers?[0];

// Com null-coalescing
int len = customers?.Length ?? 0;

// Eventos (thread-safe)
PropertyChanged?.Invoke(this, args);
```

**Arquivo:** [C6-null-conditional-operator.linq](C6-null-conditional-operator.linq)

---

### Expression-Bodied Members

**Antes (C# 5):** Corpo completo com `return` e chaves.

```csharp
public int Soma(int a, int b)
{
    return a + b;
}

public string NomeCompleto
{
    get { return Nome + " " + Sobrenome; }
}
```

**Depois (C# 6):** Sintaxe com `=>` para métodos e propriedades.

```csharp
public int Soma(int a, int b) => a + b;
public string NomeCompleto => Nome + " " + Sobrenome;
public void Imprimir() => Console.WriteLine(NomeCompleto);
```

No C# 6: métodos, propriedades somente leitura e indexadores. No C# 7+: construtores, finalizadores, acessadores.

**Arquivo:** [C6-expression-bodied-members.linq](C6-expression-bodied-members.linq)

---

### Exception Filters

**Antes (C# 5):** Capturar e re-lançar com `if`, o que altera o stack trace.

```csharp
catch (MinhaExcecao ex)
{
    if (ex.Codigo == 42)
        // tratar
    else
        throw;  // Stack trace é afetado
}
```

**Depois (C# 6):** Filtro com `when` – a pilha permanece intacta.

```csharp
catch (MinhaExcecao ex) when (ex.Codigo == 42)
{
    // Tratar apenas quando Codigo == 42
}
```

O filtro é avaliado **antes** de desenrolar a pilha. Também usado para logging sem capturar: `when (Log(e))` onde `Log` retorna `false`.

**Arquivo:** [C6-new-away-for-exception-filters.linq](C6-new-away-for-exception-filters.linq)

---

### Await in Catch and Finally

**Antes (C# 5):** `await` não era permitido em `catch` e `finally`.

```csharp
// Workaround: variável e lógica extra
Resource res = null;
try
{
    res = await Resource.OpenAsync();
}
catch (ResourceException e)
{
    // Não podia: await Resource.LogAsync(e);
    Task.Run(() => Resource.LogAsync(e)).Wait();
}
```

**Depois (C# 6):** `await` permitido em ambos.

```csharp
try
{
    await OperacaoAsync();
}
catch (Exception ex)
{
    await LogErrorAsync(ex);
}
finally
{
    await CleanupAsync();
}
```

**Arquivo:** [C6-await-in-catch-finally.linq](C6-await-in-catch-finally.linq)

---

### Parameterless Constructors in Structs

**Antes (C# 5):** Structs não podiam ter construtor sem parâmetros.

**Depois (C# 6):** Construtor sem parâmetros permitido.

```csharp
struct Person
{
    public string Name { get; }
    public int Age { get; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public Person() : this("Jane Doe", 37) { }
}
```

**Importante:** `new Person()` chama o construtor; `default(Person)` e elementos de array **não** chamam.

---

### Extension Add em Collection Initializers

**Antes (C# 5):** Apenas métodos `Add` de instância eram reconhecidos em inicializadores de coleção.

**Depois (C# 6):** Métodos `Add` de extensão também são reconhecidos. Exemplo: uma coleção customizada pode ter `Add` como método de extensão e funcionar com a sintaxe de inicializador.

---

## Melhorias no Compilador (não são features de linguagem)

- **Overload resolution** – Melhor resolução entre overloads (nullable, method groups)
- **Roslyn** – Compilador C# reescrito em C#, disponível como serviço

---

## Referências

- [nameof - Microsoft Docs](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/operators/nameof)
- [String interpolation - Microsoft Docs](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/tokens/interpolated)
- [Null-conditional operators - Microsoft Docs](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-)
- [Expression-bodied members - Microsoft Docs](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/operators/lambda-operator#expression-body-definition)
- [Exception filters (when) - Microsoft Docs](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/when)
- [using static - Microsoft Docs](https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/using-directive)
