using TestConsole.Interfaces;

namespace TestConsole.Commands.IdentityService
{
	internal class AuthorisationWithPhone : IConsoleCommand
	{
		public string? ConsoleGroupName => "IdentityService";

		public string Name => "autPhone";

		public string Description => "autPhone [phoneNumber] [password]";

		public Task<string> Execute(params string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
