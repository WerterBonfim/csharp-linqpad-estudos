<Query Kind="Statements" />

using static System.Console;

var pedro = new Funcionario();

WriteLine("{0} : {1}", nameof(Funcionario.Nome), pedro.Nome);
WriteLine("{0} : {1}", nameof(Funcionario.Idade), pedro.Idade);
WriteLine("{0} : {1}", nameof(Funcionario.Salario), pedro.Salario);
WriteLine("{0} : {1}", nameof(Funcionario.Profissao), pedro.Profissao);



class Funcionario
{
	public string Nome 		{ get; set; } = "Pedro";
	public int Idade 		{ get; set; } = 40;
	public decimal Salario 	{ get; set; } = 1000;
	public string Profissao { get; set; } = "Pescador";
}