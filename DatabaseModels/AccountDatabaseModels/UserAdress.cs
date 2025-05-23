using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels.AccountDatabaseModels
{
	[Table("user_adress")]
	public class UserAdress
	{
		#region Properties
		[Column("user_id")]
		[Required(ErrorMessage = "Не указан ID клиента")]
		public Guid UserId { get; set; } = default!;

		[Column("adress_id")]
		[Required(ErrorMessage = "Не указан ID адреса")]
		public Guid AdresssId { get; set; } = default!;

		[Column("id_default")]
		public bool IsDefault { get; set; }
		#endregion

		#region Navigation Properties
		public User User { get; set; } = null!;
		public Adress Adress { get; set; } = null!;
		#endregion
	}

}
