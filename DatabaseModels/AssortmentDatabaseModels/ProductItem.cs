using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels.AssortmentDatabaseModels
{
    [Table("product_item")]
    public class ProductItem
    {
		#region Properties
		[Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("product_id")]
		[Required(ErrorMessage = "Не указан ID продукта")]
		public long ProductId { get; set; } = default!;
        [Column("cooking_time")]
        public int CookingTime { get; set; } = default!;
        [Column("price")]
        public decimal Price { get; set; } = default!;
		[Column("image_link")]
		public string? Imagelink { get; set; }
		#endregion

		#region Navigation Properties
		public Product Product { get; set; } = null!;
        public ICollection<ProductConfiguration> Configurations { get; set; } = [];
		#endregion
	}
}


