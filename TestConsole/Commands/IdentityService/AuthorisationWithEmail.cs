using TestConsole.Interfaces;

namespace TestConsole.Commands.IdentityService
{
	internal class AuthorisationWithEmail : IConsoleCommand
	{
		public string? ConsoleGroupName => "IdentityService";

		public string Name => "authEmail";

		public string Description => "authEmail [email] [password]";

		public Task<string> Execute(params string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
