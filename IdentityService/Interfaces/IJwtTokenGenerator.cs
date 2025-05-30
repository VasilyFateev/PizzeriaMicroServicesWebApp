namespace IdentityService.Interfaces
{
	public interface IJwtTokenGenerator
	{
		string GetToken(string providedLogin);
	}
}