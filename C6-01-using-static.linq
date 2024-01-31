<Query Kind="Program" />

using static System.Console;
using static UserQuery.UseStatic;

void Main()
{
	WriteLine("Main main");
	MethodStatic();
}

public static class UseStatic
{
	public static void MethodStatic() => WriteLine("Sou o UseStatic");
}
