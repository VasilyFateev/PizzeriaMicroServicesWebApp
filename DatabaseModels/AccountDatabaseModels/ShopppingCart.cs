using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsModelClasses
{
	[Table("shopping_cart")]
	public class ShopppingCart
	{
		#region Properties
		[Key]
		[Column("id")]
		public Guid Id { get; set; } = default!;

		[Column("user_id")]
		[Required(ErrorMessage = "Не указан ID пользовтеля")]
		public Guid UserId { get; set; } = default!;
		#endregion

		#region Navigation Properties
		public ICollection<ShopppingCartItem> Items { get; set; } = [];
		public User User { get; set; } = null!;
		#endregion
	}

}
