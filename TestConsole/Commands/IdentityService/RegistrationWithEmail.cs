using TestConsole.Interfaces;

namespace TestConsole.Commands.IdentityService
{
	internal class RegistrationWithEmail : IConsoleCommand
	{
		public string? ConsoleGroupName => "IdentityService";

		public string Name => "regEmail";

		public string Description => "regEmail [email] [password]";

		public async Task<string> Execute(params string[] args)
		{
			if(args.Length != 2)
			{
				return $"[{MessageType.ERROR}] Incorrent arguments count. Correct syntax: regEmail [email] [password]";
			}
			return null;
		}
	}
}
