using System.Text.RegularExpressions;

namespace IdentityService.ServiceComponents
{
	internal interface IRegistrationProvider
	{
		public Task<IResult> Registrate();
	}

	internal partial class RegistrateService(string providedEmailOrPhone, string providedPassword) : IRegistrationProvider
	{
		const string EMAILPATTERN = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
		const string PHONEPATTERN = @"^(\+7|8)[\s(-]?\d{3}[)\s-]?\d{3}[\s-]?\d{2}[\s-]?\d{2}$|^\d{3}[\s-]?\d{2}[\s-]?\d{2}$";
		public async Task<IResult> Registrate()
		{
			if (string.IsNullOrEmpty(providedEmailOrPhone))
				return Results.BadRequest(providedEmailOrPhone);

			if (string.IsNullOrEmpty(providedPassword))
				return Results.BadRequest(providedPassword);

			if(Regex.Match(providedEmailOrPhone, EMAILPATTERN).Success)
			{
				return await RegistrateWithEmail(providedEmailOrPhone, providedPassword);
			}
			else if(Regex.Match(providedEmailOrPhone, PHONEPATTERN).Success)
			{
				return await RegistrateWithPhone(providedEmailOrPhone, providedPassword);
			}
			else
			{
				return Results.BadRequest(providedEmailOrPhone);
			}
		}

		private async Task<IResult> RegistrateWithEmail(string providedEmail, string providedPassword)
		{
			return Results.Ok();
		}

		private async Task<IResult> RegistrateWithPhone(string providedPhone, string providedPassword)
		{
			return Results.Ok();
		}
	}
}
