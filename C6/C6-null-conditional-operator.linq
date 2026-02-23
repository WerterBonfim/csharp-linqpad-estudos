<Query Kind="Program" />

void Main()
{
	// Operador ?. - acesso seguro a membros
	string[] array = null;
	array?.Length.Dump("Length (array null)"); // null

	array = ["a", "b", "c"];
	array?.Length.Dump("Length (array preenchido)"); // 3

	// Encadeamento
	var cliente = new Cliente
	{
		Endereco = new Endereco { Cep = "12345-678" }
	};
	cliente?.Endereco?.Cep.Dump("CEP do cliente");

	// Cliente sem endereço
	var clienteSemEndereco = new Cliente();
	clienteSemEndereco?.Endereco?.Cep.Dump("CEP (cliente sem endereço)"); // null

	// Operador ?[] - acesso seguro a índices
	string? primeiro = array?[0];
	primeiro.Dump("Primeiro elemento");

	string[] arrayNull = null;
	arrayNull?[0].Dump("Primeiro (array null)"); // null

	// Combinando com ?? (null-coalescing)
	int length = array?.Length ?? 0;
	length.Dump("Length com default 0");

	// Padrão para eventos (thread-safe)
	var vm = new ViewModel();
	vm.Nome = "Teste";
	vm.RaisePropertyChanged(); // Não lança se não houver inscritos
}

public class Cliente
{
	public Endereco Endereco { get; set; }
}

public class Endereco
{
	public string Cep { get; set; } = string.Empty;
}

public class ViewModel
{
	public event EventHandler<string> PropertyChanged;
	public string Nome { get; set; } = string.Empty;

	public void RaisePropertyChanged()
	{
		PropertyChanged?.Invoke(this, Nome);
	}
}
