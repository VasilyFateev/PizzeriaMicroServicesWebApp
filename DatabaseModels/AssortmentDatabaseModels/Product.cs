using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModelClasses
{
	[Table("product")]
    public class Product
	{
		#region Properties
		[Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("category_id")]
		[Required(ErrorMessage = "Не указан ID категориии")]
		public long CategoryId { get; set; } = default!;

        [Column("name")]
		[Required(ErrorMessage = "Не указано название")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Название должно содержать от 5 до 50 символов")]
		public string Name { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

		[Column("image_link")]
		public string? Imagelink { get; set; }
		#endregion

		#region Navigation Properties
		public Category Category { get; set; } = null!;
        public ICollection<ProductItem> ProductItems { get; set; } = null!;
		#endregion
	}
}
