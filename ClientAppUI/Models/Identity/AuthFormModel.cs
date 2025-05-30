using ClientAppUI.Utils.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ClientAppUI.Models.Identity
{
	public class AuthFormModel
	{
		[Display(Name = "Email or Phone")]
		[Required(ErrorMessage = "Введите email или номер телефона")]
		[EmailOrPhone(ErrorMessage = "Введите email или номер телефона")]
		public string Login { get; set; } = string.Empty;

		[Required(ErrorMessage = "Введите пароль")]
		[StringLength(50, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 50 символов")]
		public string Password { get; set; } = string.Empty;
	}
}
