<Query Kind="Program" />

void Main()
{
	var calc = new Calculadora();
	calc.Soma(2, 3).Dump("Soma");

	var pessoa = new Pessoa("João", "Silva");
	pessoa.NomeCompleto.Dump("Nome Completo");
	pessoa.DizerOi();
}

// C# 6: Expression-bodied members - métodos e propriedades com =>
public class Calculadora
{
	public int Soma(int a, int b) => a + b;
	public int Subtracao(int a, int b) => a - b;
}

public class Pessoa
{
	public string Nome { get; }
	public string Sobrenome { get; }

	public Pessoa(string nome, string sobrenome)
	{
		Nome = nome;
		Sobrenome = sobrenome;
	}

	// Propriedade somente leitura com expression-bodied
	public string NomeCompleto => $"{Nome} {Sobrenome}";

	// Método void com expression-bodied
	public void DizerOi() => Console.WriteLine($"Olá, {NomeCompleto}!");

	// Indexador com expression-bodied
	private readonly string[] _dados = new[] { "João", "Maria", "Pedro" };
	public string this[int index] => _dados[index];
}
