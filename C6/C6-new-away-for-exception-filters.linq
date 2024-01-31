<Query Kind="Program" />


//   Permite especificar uma condição no bloco catch, se for verdadeiro, será executado o catch: 
void Main()
{
	int 
		valorA = 7,
		valorB = 0,
		resultado = 0;
	
	try
	{	        
		resultado = valorA / valorB;
	}
	catch (Exception ex) when (valorB == 0)
	{
		Console.WriteLine("Não pode dividir um valor por 0");
	}
	catch (Exception ex)
	{
		throw;
	}
}
