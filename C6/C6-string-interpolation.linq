<Query Kind="Program" />

void Main()
{
	var nome = "Mundo";
	var idade = 25;

	// C# 6: String interpolation com $"
	var saudacao = $"Olá, {nome}!";
	saudacao.Dump("Saudação simples");

	// Expressões dentro das chaves
	var mensagem = $"{nome} tem {idade} ano(s)";
	mensagem.Dump("Mensagem com idade");

	// Formatação (alinhamento e especificadores)
	var formatado = $"{nome,-20} tem {idade:D3} anos";
	formatado.Dump("Com formatação");

	// Expressões complexas
	int quantidade = 2;
	decimal preco = 50.5m;
	var total = $"Total: {quantidade * preco:C}";
	total.Dump("Total formatado como moeda");

	// Condicional dentro da interpolação (parenteses para evitar confusão com :)
	var plural = $"Ano{(idade == 1 ? "" : "s")}";
	plural.Dump("Plural condicional");

	// Comparação: Antes (C# 5) vs Depois (C# 6)
	// Antes: String.Format("{0} tem {1} anos", nome, idade)
	// Depois: $"{nome} tem {idade} anos"
}
