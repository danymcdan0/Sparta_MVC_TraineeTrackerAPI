using Microsoft.AspNetCore.Identity;

namespace WebAppGroup1.Models
{
	public class Spartan : IdentityUser
	{
		public List<Tracker>? Trackers { get; set; }
	}
}
