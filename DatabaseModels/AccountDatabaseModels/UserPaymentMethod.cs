using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsModelClasses
{
	[Table("user_payment_method")]
	public class UserPaymentMethod
	{
		#region Properties
		[Key]
		[Column("id")]
		public Guid Id { get; set; } = default!;
		[Column("user_id")]
		public Guid UserId { get; set; } = default!;
		[Column("payment_agregator_user_id")]
		public string PaymentAgregatorCardId { get; set; } = default!;
		[Column("bank_card_last_numbers")]
		public string BankCardLastNumbers { get; set; } = default!;
		[Column("id_default")]
		public bool IsDefault  { get; set; }
		#endregion

		#region Navigation Properties
		public User User { get; set; } = null!;
		#endregion
	}

}
