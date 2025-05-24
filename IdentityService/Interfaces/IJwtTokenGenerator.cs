namespace IdentityService.Utils
{
	public interface IJwtTokenGenerator
	{
		string GetToken(string providedLogin);
	}
}