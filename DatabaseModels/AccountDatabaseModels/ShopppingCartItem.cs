using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels.AccountDatabaseModels
{
	[Table("shopping_cart_item")]
	public class ShopppingCartItem
	{
		#region Properties
		[Key]
		[Column("id")]
		public Guid Id { get; set; } = default!;

		[Column("cart_id")]
		[Required(ErrorMessage = "Не указан ID корзины")]
		public Guid CartId { get; set; } = default!;

		[Column("product_item_id")]
		[Required(ErrorMessage = "Не указан ID единицы продукта")]
		public long ProductItemId { get; set; } = default!;

		[Column("count")]
		[MinLength(1, ErrorMessage = "Указано недопустимое количество товара")]
		public long Count { get; set; } = 1;
		#endregion

		#region Navigation Properties
		public ShopppingCart ShopppingCart { get; set; } = null!;
		#endregion
	}

}
