using Dumpify;

// --- Demonstração ---

"C# 13: Extension Methods".Dump();

var list = new List<int> { 1, 2, 3 };
var emptyList = new List<int>();

new {
    List = list,
    IsEmpty = list.IsEmptyTraditional(),
    EmptyList = emptyList,
    IsEmpty_Empty = emptyList.IsEmptyTraditional()
}.Dump("Tradicional Extension Methods (C# 13)");


"\nC# 14: Extension Types (Preview)".Dump();

int[] data = [1, 2, 3];
int[] emptyData = [];

// Usando membros de instância da extensão (Propriedades e Métodos)
new {
    Data = data,
    IsEmpty = data.IsEmpty, // Propriedade de extensão (C# 14)
    HasAny = data.HasAny()   // Método de extensão (estilo instância)
}.Dump("Extension Types - Instance Members (C# 14)");

// Usando membros estáticos da extensão (Novo no C# 14)
IEnumerable<int>.Identity.Dump("Extension Types - Static Member (Identity)");

// Usando operador da extensão (Novo no C# 14)
var combined = data + [4, 5];
combined.Dump("Extension Types - Operator (+) Extension");


// C# 13: Métodos de extensão tradicionais
public static class EnumerableExtensions13
{
    public static bool IsEmptyTraditional<T>(this IEnumerable<T> source) => !source.Any();
}

// C# 14: Extension Types (preview)
public static class EnumerableExtensions14
{
    // Extensão para membros de instância
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmpty => !source.Any();
        public bool HasAny() => source.Any();
    }

    // Extensão para membros estáticos
    extension<TSource>(IEnumerable<TSource>)
    {
        public static IEnumerable<TSource> Identity => Enumerable.Empty<TSource>();
        
        public static IEnumerable<TSource> operator +(IEnumerable<TSource> left, IEnumerable<TSource> right)
            => left.Concat(right);
    }
}

