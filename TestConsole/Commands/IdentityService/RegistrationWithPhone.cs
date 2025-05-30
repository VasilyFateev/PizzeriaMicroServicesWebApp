using TestConsole.Interfaces;

namespace TestConsole.Commands.IdentityService
{
	internal class RegistrationWithPhone : IConsoleCommand
	{
		public string? ConsoleGroupName => "IdentityService";

		public string Name => "regPhone";

		public string Description => "regPhone [phoneNumber] [password]";

		public Task<string> Execute(params string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
