using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppGroup1.Models;

namespace WebAppGroup1.Data
{
	public class SpartaTrackerContext : IdentityDbContext
	{
		public SpartaTrackerContext(DbContextOptions<SpartaTrackerContext> options) : base(options)
		{
		
		}
		public DbSet<Tracker> TrackerEntries { get; set; }
		public DbSet<Spartan> Spartans { get; set; }
	}
}
