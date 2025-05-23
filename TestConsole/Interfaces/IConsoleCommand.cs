namespace TestConsole.Interfaces
{
	public interface IConsoleCommand
	{
		string? ConsoleGroupName { get; }
		string Name { get; }
		string Description { get; }
		Task<string> Execute(params string[] args);
	}
}
