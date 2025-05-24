using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IdentityService.Utils
{
	public class DefaultJwtTokenGenerator : IJwtTokenGenerator
	{
		public string GetToken(string providedLogin)
		{
			var claims = new List<Claim> { new(ClaimTypes.Name, providedLogin) };
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					claims: claims,
					expires: DateTime.UtcNow.AddHours(1),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}
	}
}
