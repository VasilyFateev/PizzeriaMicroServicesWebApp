using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModelClasses
{
    [Table("category")]
    public class Category
    {
		#region Properties
		[Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
		[Required(ErrorMessage = "Не указано название")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Название должно содержать от 5 до 50 символов")]
		public string Name { get; set; } = null!;
		#endregion

		#region Navigation Properties
		public ICollection<Product> Products { get; set; } = null!;
        public ICollection<Variation> Variations { get; set; } = [];
		#endregion
	}

}
