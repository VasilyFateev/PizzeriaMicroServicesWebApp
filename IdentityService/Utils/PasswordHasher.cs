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
	}
}
