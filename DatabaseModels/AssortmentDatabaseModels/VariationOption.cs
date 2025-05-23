using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels.AssortmentDatabaseModels
{
    [Table("variation_option")]
    public class VariationOption
    {
		#region Properties
		[Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("variation_id")]
		[Required(ErrorMessage = "Не указан ID вариации")]
		public long VariationId { get; set; } = default!;

        [Column("name")]
		[Required(ErrorMessage = "Не указано название")]
		[StringLength(50, MinimumLength = 5, ErrorMessage = "Название должно содержать от 5 до 50 символов")]
        public string Name { get; set; } = null!;
		#endregion

		#region Navigation Properties
		public Variation Variation { get; set; } = null!;
        public ICollection<ProductConfiguration> Configurations { get; set; } = [];
		#endregion
	}

}
