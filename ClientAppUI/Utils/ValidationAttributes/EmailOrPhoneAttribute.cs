using System.ComponentModel.DataAnnotations;

namespace ClientAppUI.Utils.ValidationAttributes
{
	public class EmailOrPhoneAttribute : ValidationAttribute
	{
		public override bool IsValid(object? value) => value switch
		{
			string valueString when valueString != string.Empty => ClientAppRegexCollection.EmailOrPhoneRegex().Match(valueString).Success,
			_ => false
		};
	}
}