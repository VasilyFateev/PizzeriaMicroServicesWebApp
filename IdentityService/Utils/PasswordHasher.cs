using DatabaseModels.AccountDatabaseModels;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Utils
{
	public static class PasswordHasher
	{
		public static string GetHashedPassword(User user, string providedPassword)
		{
			PasswordHasher<User> passwordHasher = new();
			return passwordHasher.HashPassword(user, providedPassword);
		}

		public static bool VerifyHashedPassword(User user, string providedPassword)
		{
			PasswordHasher<User> passwordHasher = new();
			return passwordHasher.VerifyHashedPassword(user, user.HashedPassword, providedPassword) switch
			{
				PasswordVerificationResult.Success => true,
				PasswordVerificationResult.SuccessRehashNeeded => true,
				_ => false,
			};
		}
	}
}
