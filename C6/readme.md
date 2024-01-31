# Funcionalidade do C# 6 - 2015 .Net Framework 4.6

1. **Inicializadores de propriedade auto-implementadas**: Permite definir um valor inicial para propriedades auto-implementadas diretamente na sua declaração.
   ```csharp
   public string Nome { get; set; } = "Nome Padrão";
   ```

2. **Métodos de extensão para inicializadores de coleção**: Permite adicionar métodos de extensão para inicializadores de coleção.
   ```csharp
   var numbers = new List<int> { 1, 2, 3 }.AddRange(new[] { 4, 5 });
   ```

3. **Expressões nameof**: Permite obter o nome de uma variável, propriedade ou membro como uma string em tempo de compilação.
   ```csharp
   Console.WriteLine(nameof(Nome)); // Saída: "Nome"
   ```

4. **Interpolação de strings**: Simplifica a forma como as strings são formatadas, permitindo inserir expressões diretamente dentro de uma string literal.
   ```csharp
   var nome = "Mundo";
   var saudacao = $"Olá, {nome}!"; // Saída: "Olá, Mundo!"
   ```

5. **Operadores null condicional**: Reduz a necessidade de verificações repetidas de null e encadeia operações se o valor não for null.
   ```csharp
   var tamanho = cliente?.Endereco?.Cidade?.Length;
   ```

6. **Expressões de inicialização de índice e dicionário**: Permite usar índices e chaves de dicionário em inicializadores de coleção.
   ```csharp
   var dict = new Dictionary<int, string>
   {
       [1] = "um",
       [2] = "dois",
       // ...
   };
   ```

7. **Filtros de exceção**: Permite especificar uma condição em um bloco catch.
   ```csharp
   try
   {
       // Código que pode lançar uma exceção
   }
   catch (MeuTipoDeExcecao e) when (e.Codigo == 42)
   {
       // Tratamento específico para a exceção com código 42
   }
   ```

8. **Operador de atribuição null-coalescente**: Atribui o valor à variável somente se a variável é null.
   ```csharp
   x ??= ComputeValue();
   ```

9. **Membros com expressão-bodied**: Permite definir métodos e propriedades de uma linha usando expressões lambda.
   ```csharp
   public string NomeCompleto => $"{Nome} {Sobrenome}";
   public void DizerOi() => Console.WriteLine("Oi");
   ```

10. **Await em blocos catch e finally**: Permite o uso do operador await dentro dos blocos catch e finally.
    ```csharp
    try
    {
        await OperacaoAsync();
    }
    catch (Exception ex)
    {
        await LogAsync(ex);
    }
    finally
    {
        await CleanupAsync();
    }
    ```

Essas são algumas das funcionalidades que foram introduzidas no C# 6, e elas ajudaram a tornar o código mais legível e a reduzir a verbosidade em muitos casos comuns.

# Descrição

## Use Static

A funcionalidade `using static` no C# 6 permite que você importe membros estáticos de uma classe para que você possa usá-los em seu código sem a necessidade de qualificar o acesso com o nome da classe. Isso torna o código mais limpo e menos verboso, especialmente quando você está trabalhando com classes que contêm muitos membros estáticos úteis, como a classe `System.Math` ou a classe `System.Console`.

Antes do C# 6, se você quisesse chamar um método estático ou acessar uma propriedade estática, você teria que incluir o nome da classe cada vez que chamasse o método ou a propriedade. Por exemplo:

```csharp
Console.WriteLine(Math.Sqrt(4));
```

Com o `using static`, você pode importar os membros estáticos da classe `System.Math` e `System.Console` para o escopo do arquivo e usá-los diretamente sem o nome da classe:

```csharp
using static System.Math;
using static System.Console;

class Program
{
    static void Main()
    {
        WriteLine(Sqrt(4)); // Agora você pode chamar Sqrt e WriteLine diretamente
    }
}
```

Isso simplifica o código e torna mais fácil de ler e escrever. A diretiva `using static` pode ser particularmente útil para classes de utilitários e constantes definidas em classes estáticas, permitindo que você as acesse de forma mais direta e concisa.

## Auto property initializer

A funcionalidade de inicializadores de propriedade automática (auto-property initializer) no C# 6 permite que você atribua valores iniciais às propriedades auto-implementadas no momento da sua declaração. Antes do C# 6, para inicializar uma propriedade auto-implementada com um valor padrão, você precisava fazer isso no construtor da classe. Com essa nova funcionalidade, você pode simplificar o código e torná-lo mais legível.

Aqui está um exemplo de como as propriedades auto-implementadas eram inicializadas antes do C# 6:

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

Com a introdução dos inicializadores de propriedade automática no C# 6, você pode atribuir valores iniciais diretamente na declaração da propriedade, eliminando a necessidade de um construtor apenas para esse fim:

```csharp
public class Pessoa
{
    public string Nome { get; set; } = "Nome Padrão";
    public int Idade { get; set; } = 0;
}
```

Essa funcionalidade torna o código mais conciso e mantém a inicialização próxima à declaração da propriedade, o que pode melhorar a legibilidade. Além disso, ajuda a evitar erros, pois você não precisa se preocupar em esquecer de inicializar a propriedade no construtor ou em outros métodos da classe.

## Dictionary Initializer

O inicializador de dicionário (Dictionary Initializer) aprimorado no C# 6 é uma extensão dos inicializadores de coleção que já existiam em versões anteriores do C#. Essa funcionalidade permite que você inicialize dicionários de uma maneira mais concisa e legível, utilizando uma sintaxe similar à de inicializadores de objetos.

Antes do C# 6, para inicializar um dicionário, você poderia fazer algo assim:

```csharp
var dicionario = new Dictionary<int, string>
{
    { 1, "um" },
    { 2, "dois" },
    { 3, "três" }
};
```

Com a introdução dos inicializadores de dicionário no C# 6, você pode inicializar pares chave-valor de uma maneira mais simplificada, sem a necessidade de repetir a palavra-chave `Add`. A nova sintaxe permite que você use índices diretamente:

```csharp
var dicionario = new Dictionary<int, string>
{
    [1] = "um",
    [2] = "dois",
    [3] = "três"
};
```

Neste exemplo, a sintaxe `[chave] = valor` é utilizada para adicionar elementos ao dicionário. Isso torna o código mais limpo e mais próximo da forma como você acessa ou atribui valores em um dicionário já existente.

Essa funcionalidade é particularmente útil quando você está inicializando um dicionário com um conjunto conhecido de pares chave-valor e deseja fazê-lo de uma maneira que seja fácil de ler e manter.


## Nameof Expression:

A expressão `nameof` foi introduzida no C# 6 como uma maneira de obter o nome simples (não qualificado) de uma variável, tipo ou membro como uma string literal em tempo de compilação. Isso é útil em várias situações, especialmente para evitar erros de digitação em strings que se referem a nomes de membros de código e para facilitar a refatoração, pois as referências de nome são atualizadas automaticamente se o nome do membro do código mudar.

Antes do C# 6, se você quisesse referenciar o nome de uma propriedade ou método como uma string, você teria que digitá-lo manualmente, o que poderia levar a erros se o nome do membro fosse alterado e a string não fosse atualizada adequadamente. Por exemplo:

```csharp
public class Pessoa
{
    public string Nome { get; set; }

    public void ImprimirNome()
    {
        Console.WriteLine("Nome"); // Erro se a propriedade Nome for renomeada
    }
}
```

Com a introdução da expressão `nameof`, você pode fazer isso de forma segura:

```csharp
public class Pessoa
{
    public string Nome { get; set; }

    public void ImprimirNome()
    {
        Console.WriteLine(nameof(Nome)); // Saída: "Nome"
    }
}
```

Se a propriedade `Nome` for renomeada, a expressão `nameof(Nome)` será automaticamente atualizada para refletir o novo nome, o que reduz a chance de erros e torna o código mais manutenível.

Aqui estão alguns exemplos de como a expressão `nameof` pode ser usada:

```csharp
// Nome de uma propriedade
Console.WriteLine(nameof(Pessoa.Nome)); // Saída: "Nome"

// Nome de um método
Console.WriteLine(nameof(Pessoa.ImprimirNome)); // Saída: "ImprimirNome"

// Nome de uma variável local
string variavelLocal = "valor";
Console.WriteLine(nameof(variavelLocal)); // Saída: "variavelLocal"

// Nome de um tipo
Console.WriteLine(nameof(Pessoa)); // Saída: "Pessoa"

// Nome de um parâmetro
void Metodo(string parametro)
{
    Console.WriteLine(nameof(parametro)); // Saída: "parametro"
}
```

A expressão `nameof` é particularmente útil para notificar mudanças de propriedades em padrões como INotifyPropertyChanged, para validação de parâmetros e para levantar exceções com nomes de membros precisos.


## New way for Exception filters

Os filtros de exceção são uma adição ao C# 6 que permite que você especifique uma condição de filtro em um bloco `catch`. Essa condição determina se o bloco `catch` específico deve ser executado com base na avaliação da expressão fornecida. Isso é útil para capturar exceções de forma mais granular, sem ter que adicionar lógica adicional dentro do bloco `catch`.

Antes do C# 6, se você quisesse executar um bloco `catch` com base em uma condição específica, você teria que capturar a exceção e, em seguida, usar um `if` ou alguma outra lógica condicional para verificar a condição:

```csharp
try
{
    // Código que pode lançar exceção
}
catch (MinhaExcecao ex)
{
    if (ex.Codigo == 42)
    {
        // Tratar a exceção com código 42
    }
    else
    {
        throw; // Re-lançar a exceção se a condição não for atendida
    }
}
```

Com a introdução dos filtros de exceção no C# 6, você pode agora escrever o mesmo código de forma mais concisa e legível:

```csharp
try
{
    // Código que pode lançar exceção
}
catch (MinhaExcecao ex) when (ex.Codigo == 42)
{
    // Tratar a exceção apenas se o código for 42
}
```

Neste exemplo, o bloco `catch` só será executado se a exceção capturada for do tipo `MinhaExcecao` e o valor da propriedade `Codigo` for 42. Se a condição do filtro não for atendida, a exceção não será capturada por esse bloco `catch` e poderá ser capturada por um bloco `catch` subsequente ou propagada para cima na pilha de chamadas.

Os filtros de exceção são especialmente úteis porque a expressão de filtro é avaliada antes de a pilha de chamadas ser desenrolada. Isso significa que você tem acesso ao estado do programa no momento exato em que a exceção foi lançada, o que pode ser valioso para fins de diagnóstico e depuração.


## Await in catch and finally block

No C# 6, uma das adições importantes foi a capacidade de usar `await` dentro dos blocos `catch` e `finally`. Antes dessa atualização, você não poderia realizar operações assíncronas dentro desses blocos de tratamento de exceção. Isso limitava a maneira como você poderia lidar com erros e limpezas em operações assíncronas.

Agora, com o C# 6, você pode aguardar tarefas dentro de `catch` e `finally`, o que é muito útil para operações de limpeza ou registro de erros que são assíncronos. Por exemplo, você pode querer registrar um erro em um arquivo de log ou em um banco de dados que requer uma chamada de rede ou IO, que são naturalmente operações assíncronas.

Aqui está um exemplo de como você pode usar `await` em blocos `catch` e `finally`:

```csharp
public async Task MetodoAssincrono()
{
    try
    {
        // Código que pode lançar uma exceção assincronamente
        await AlgumaOperacaoAssincrona();
    }
    catch (Exception ex)
    {
        // Agora você pode realizar operações assíncronas no bloco catch
        await LogErrorAsync(ex);
    }
    finally
    {
        // E também no bloco finally
        await CleanupAsync();
    }
}

public async Task LogErrorAsync(Exception ex)
{
    // Implementação do método de registro de erros
}

public async Task CleanupAsync()
{
    // Implementação do método de limpeza
}
```

Essa funcionalidade tornou o C# mais flexível e poderoso no que diz respeito ao tratamento de operações assíncronas, permitindo que os desenvolvedores escrevam código de tratamento de exceções que é tanto robusto quanto eficiente.



## Null-Conditionall Operator

O operador condicional nulo (Null-Conditional Operator), também conhecido como o operador de acesso condicional ou "Elvis operator", foi introduzido no C# 6.0. Este operador permite que você acesse membros ou elementos de um objeto somente se o objeto não for nulo, ajudando a evitar a exceção `NullReferenceException`.

O operador condicional nulo é representado por `?.` e pode ser usado com membros de objetos (como propriedades ou métodos) e também com índices de elementos em arrays ou coleções.

Aqui está um exemplo do uso do operador condicional nulo:

```csharp
string[] array = null;

// Sem o operador condicional nulo, você teria que verificar explicitamente se o array não é nulo
if (array != null && array.Length > 0)
{
    Console.WriteLine(array[0]);
}

// Com o operador condicional nulo, você pode simplificar o código:
Console.WriteLine(array?.Length); // Não faz nada se array for nulo, evitando uma NullReferenceException

// Funciona também com métodos
string valor = array?.FirstOrDefault(); // Retorna null se array for nulo
```

Quando o operador condicional nulo é usado e o objeto à esquerda dele é `null`, a expressão inteira avalia como `null` e a parte à direita do operador não é avaliada. Isso significa que você pode encadear várias chamadas com o operador condicional nulo:

```csharp
// Considerando que pessoa pode ser nulo e Endereco também pode ser nulo
string cep = pessoa?.Endereco?.CEP; // Retorna null se pessoa ou Endereco for nulo
```

Além disso, há uma variação do operador condicional nulo para indexadores, que é `?[`. Isso é útil para acessar elementos em arrays ou coleções que podem ser nulos:

```csharp
string valor = array?[0]; // Retorna null se array for nulo, caso contrário, retorna o elemento no índice 0
```

O operador condicional nulo é uma ferramenta poderosa para escrever código mais limpo e seguro, reduzindo a necessidade de verificações explícitas de nulidade e protegendo contra exceções indesejadas.



## Expression – bodied Methods

Os métodos com expressão-bodied (expression-bodied methods) são uma das funcionalidades introduzidas no C# 6 que permitem que você defina métodos com uma sintaxe mais concisa, usando uma expressão lambda em vez de um bloco de código tradicional. Essa funcionalidade é particularmente útil para métodos que consistem em uma única expressão ou instrução de retorno.

Antes do C# 6, se você quisesse criar um método simples que apenas retornasse o valor de uma expressão, você teria que usar a sintaxe completa com chaves e a instrução `return`:

```csharp
public class Calculadora
{
    public int Soma(int a, int b)
    {
        return a + b;
    }
}
```

Com a introdução dos métodos com expressão-bodied no C# 6, você pode reescrever o mesmo método de forma mais concisa:

```csharp
public class Calculadora
{
    public int Soma(int a, int b) => a + b;
}
```

Aqui, a expressão `a + b` é o corpo do método `Soma`, e o operador `=>` é usado para indicar que o método é definido por uma expressão. Não é necessário usar chaves ou a instrução `return`.

Além de métodos, a sintaxe de expressão-bodied também pode ser aplicada a propriedades somente leitura e indexadores no C# 6. No C# 7 e versões posteriores, essa sintaxe foi expandida para incluir construtores, finalizadores e acessadores de propriedades.

Aqui está um exemplo de uma propriedade somente leitura usando expressão-bodied:

```csharp
public class Pessoa
{
    public string Nome { get; }
    public string Sobrenome { get; }

    public Pessoa(string nome, string sobrenome)
    {
        Nome = nome;
        Sobrenome = sobrenome;
    }

    // Propriedade somente leitura com expressão-bodied
    public string NomeCompleto => $"{Nome} {Sobrenome}";
}
```

Essa sintaxe torna o código mais limpo e expressivo, especialmente quando o corpo do método ou propriedade é simples.


## Easily format string using String interpolation

A interpolação de strings é outra funcionalidade introduzida no C# 6 que simplifica a maneira como as strings são formatadas. Antes da interpolação de strings, a formatação de strings geralmente envolvia o uso do método `String.Format`, que pode ser verboso e propenso a erros, especialmente com múltiplos argumentos.

Com a interpolação de strings, você pode inserir diretamente as expressões C# em uma string literal, precedendo a string com o símbolo `$` e envolvendo as expressões entre chaves `{}`. O compilador transforma a string interpolada em uma chamada de `String.Format` em tempo de compilação.

Aqui está um exemplo de como a interpolação de strings pode ser usada:

Sem interpolação de strings:

```csharp
string nome = "Mundo";
string saudacao = String.Format("Olá, {0}!", nome);
Console.WriteLine(saudacao); // Saída: Olá, Mundo!
```

Com interpolação de strings:

```csharp
string nome = "Mundo";
string saudacao = $"Olá, {nome}!";
Console.WriteLine(saudacao); // Saída: Olá, Mundo!
```

A interpolação de strings também permite a execução de expressões mais complexas dentro das chaves:

```csharp
int quantidade = 2;
int preco = 50;
string mensagem = $"O total é: {quantidade * preco:C}";
Console.WriteLine(mensagem); // Saída: O total é: R$ 100,00 (assumindo que a cultura atual é pt-BR)
```

No exemplo acima, `:C` é usado para formatar o resultado da expressão como uma string de moeda, de acordo com as configurações de cultura atuais.

A interpolação de strings torna o código mais legível e fácil de entender, reduzindo a complexidade associada à concatenação de strings e formatação.
