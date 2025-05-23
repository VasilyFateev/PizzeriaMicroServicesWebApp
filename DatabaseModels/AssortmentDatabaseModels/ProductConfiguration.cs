using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModelClasses
{
	[Table("product_configuration")]
	public class ProductConfiguration
	{
		#region Properties
		[Column("item_id")]
		[Required(ErrorMessage = "Не указан ID единицы продукта")]
		public long ProductItemId { get; set; }

		[Column("option_id")]
		[Required(ErrorMessage = "Не указан ID опции вариации")]
		public long VariationOptionId { get; set; }
		#endregion

		#region Navigation Properties
		public ProductItem ProductItem { get; set; } = null!;
		public VariationOption? VariationOption { get; set; }
		#endregion
	}
}
