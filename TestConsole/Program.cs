using ConsoleView;
using TestConsole;


var parcer = new ConsoleCommandParser();
{
	while (true)
	{
		var providedString = Console.ReadLine();
		if (providedString == null)
		{
			continue;
		}
		var result = await parcer.Proccess(providedString);
		HandleLogMessage(result);
	}
}


static void HandleLogMessage(string message)
{
	var (color, action) = message switch
	{
		string s when s.StartsWith($"[{MessageType.ERROR}]") => (ConsoleColor.Red, Console.WriteLine),
		string s when s.StartsWith($"[{MessageType.WARN}]") => (ConsoleColor.Yellow, Console.WriteLine),
		string s when s.StartsWith($"[{MessageType.INFO}]") => (ConsoleColor.Green, Console.WriteLine),
		_ => (Console.ForegroundColor, (Action<string>)Console.WriteLine)
	};

	Console.ForegroundColor = color;
	action(message);
	Console.ResetColor();
}
