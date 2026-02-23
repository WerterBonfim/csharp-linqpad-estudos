<Query Kind="Program" />

void Main()
{
	MetodoAssincrono().Wait();
}

// C# 6: await permitido em blocos catch e finally
async Task MetodoAssincrono()
{
	Resource res = null;
	try
	{
		res = await Resource.OpenAsync();
		"Operação concluída".Dump();
	}
	catch (ResourceException ex)
	{
		// C# 6: Agora podemos usar await no catch
		await Resource.LogAsync(res, ex);
		"Erro logado".Dump();
	}
	finally
	{
		// C# 6: Agora podemos usar await no finally
		if (res != null)
			await res.CloseAsync();
		"Recurso fechado".Dump();
	}
}

public class Resource
{
	public static Task<Resource> OpenAsync()
	{
		return Task.FromResult(new Resource());
	}

	public Task CloseAsync()
	{
		return Task.CompletedTask;
	}
}

public static class ResourceExtensions
{
	public static Task LogAsync(Resource res, ResourceException ex)
	{
		$"Log: {ex.Message}".Dump();
		return Task.CompletedTask;
	}
}

public class ResourceException : Exception
{
	public ResourceException(string message) : base(message) { }
}
