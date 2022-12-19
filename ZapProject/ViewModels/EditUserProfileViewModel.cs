using ZapProject.Models;

namespace ZapProject.ViewModels
{
	public class EditUserProfileViewModel
	{
		public string Id { get; set; }
		public string? PhoneNumber { get; set; }
		public int? AddressId { get; set; }
		public Address? Address { get; set; }
	}
}
