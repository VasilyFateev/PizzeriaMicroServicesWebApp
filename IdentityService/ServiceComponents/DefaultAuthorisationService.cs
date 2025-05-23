using DatabasesAccess.AccountDb;
using IdentityService.Interfaces;
using IdentityService.Utils;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.ServiceComponents
{
	internal sealed class DefaultAuthorisationService(string providedEmailOrPhone, string providedPassword) : IAuthorisationProvider
	{
		public async Task<IResult> AuthorizeAsync()
		{
			if (string.IsNullOrEmpty(providedEmailOrPhone) || string.IsNullOrEmpty(providedPassword))
				return Results.Unauthorized();

			bool isEmail = RegexCollection.EmailRegex().Match(providedEmailOrPhone).Success;
			bool isPhone = RegexCollection.PhoneRegex().Match(providedEmailOrPhone).Success;

			if (!isEmail && !isPhone)
			{
				return Results.Unauthorized();
			}

			using AccountContext context = new();

			var user = await context.Users.FirstOrDefaultAsync(u => (isEmail ? u.Email : u.PhoneNumber) == providedEmailOrPhone);
			if (user == null)
			{
				return Results.Unauthorized();
			}

			var hashedPassword = PasswordHasher.GetHashedPassword(user, providedPassword);
			return user.HashedPassword == hashedPassword ? Results.Ok() : Results.Unauthorized();
		}
	}
}
