using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels.AccountDatabaseModels
{
	[Table("user")]
	[Index(nameof(Email), IsUnique = true, Name = "ix_user_email")]
	[Index(nameof(PhoneNumber), IsUnique = true, Name = "ix_user_phone_number")]
	public class User
	{
		#region Properties
		[Key]
		[Column("id")]
		public Guid Id { get; set; } = default!;

		[Column("name")]
		[Required(ErrorMessage = "Не указано название")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Название должно содержать от 5 до 50 символов")]
		public string Name { get; set; } = default!;

		[Column("phone_number")]
		[Phone(ErrorMessage = "Не корректный номер телефона")]
		public string? PhoneNumber { get; set; }

		[Column("email")]
		[EmailAddress(ErrorMessage = "Не корректный адресс электронной почты")]
		public string? Email { get; set; }

		[Column("hashed_password")]
		public string HashedPassword { get; set; } = default!;
		#endregion

		#region Properties
		public ICollection<UserAdress> UserAdresses { get; set; } = null!;
		public ICollection<UserPaymentMethod> UserPaymentMethods { get; set; } = null!;
		public ICollection<ShopppingCart> ShopppingCarts { get; set; } = null!;
		#endregion
	}
}
