<Query Kind="Statements" />


Dictionary<string, string> Versao05Forma1 = new Dictionary<string, string>()
{
	{ "Guitarra", "Fender Stratocaster Eric Johnson Signature"},
	{ "Violão", "Yamaha silent SLG200s" },
	{ "Semi-Acustica", "Ibanez GB10 George Benson" }
};

Versao05Forma1["Guitarra"] = "1964 Gibson ES-335";

Dictionary<string, string> Versao06Forma1 = new Dictionary<string, string>() 
{
	["Guitarra"] = "Fender Stratocaster Eric Johnson Signature",
	["Violão"] = "Yamaha silent SLG200s",
	["Semi-Acustic"] = "Ibanez GB10 George Benson"
};


Dictionary<int, string> Versao05Forma2 = new Dictionary<int, string>
{
	{ 1, "C#" },
	{ 2, "Sql" },
	{ 3, "F#" }
};

Dictionary<int, string> Versao06Forma2 = new Dictionary<int, string>
{
	[1] = "C#",
	[2] = "Sql",
	[3] = "F#",
};

Versao06Forma2[4] = "Redis";