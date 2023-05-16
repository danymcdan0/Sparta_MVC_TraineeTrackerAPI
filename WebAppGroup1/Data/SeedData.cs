using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using WebAppGroup1.Models;

namespace WebAppGroup1.Data
{
	public class SeedData
	{
		public static void Initialise(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<SpartaTrackerContext>();
			var userManager = serviceProvider.GetRequiredService<UserManager<Spartan>>();
			var roleStore = new RoleStore<IdentityRole>(context);

			if (context.Spartans.Any())
			{
				context.Spartans.RemoveRange(context.Spartans);
				context.TrackerEntries.RemoveRange(context.TrackerEntries);
				context.UserRoles.RemoveRange(context.UserRoles);
				context.Roles.RemoveRange(context.Roles);
				context.SaveChanges();
			}

			var trainer = new IdentityRole
			{
				Name = "Trainer",
				NormalizedName = "TRAINER"
			};
			var trainee = new IdentityRole
			{
				Name = "Trainee",
				NormalizedName = "TRAINEE"
			};

			roleStore
			.CreateAsync(trainer)
			.GetAwaiter()
			.GetResult();
			roleStore
			.CreateAsync(trainee)
			.GetAwaiter()
			.GetResult();

			var phil = new Spartan
			{
				UserName = "Phil@spartaglobal.com",
				Email = "Phil@spartaglobal.com",
				EmailConfirmed = true
			};
			var nish = new Spartan
			{
				UserName = "Nish@spartaglobal.com",
				Email = "Nish@spartaglobal.com",
				EmailConfirmed = true,
			};
			var peter = new Spartan
			{
				UserName = "Peter@spartaglobal.com",
				Email = "Peter@spartaglobal.com",
				EmailConfirmed = true
			};

			userManager
 .CreateAsync(phil, "Password1!")
 .GetAwaiter()
 .GetResult();
			userManager
			.CreateAsync(nish, "Password1!")
			.GetAwaiter()
			.GetResult();
			userManager
			.CreateAsync(peter, "Password1!")
			.GetAwaiter()
			.GetResult();

			context.UserRoles.AddRange(new IdentityUserRole<string>[]
{
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(phil).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainer).Result
			 },
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(nish).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 },
				 new IdentityUserRole<string>
				 {
			 UserId = userManager.GetUserIdAsync(peter).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 }



			 });

			context.TrackerEntries.AddRange(
 new Tracker
 {
	 Week = 1,
	 Start = "Complete the weekly survey",
	 Complete = false,
	 Spartan = nish
 },
 new Tracker
 {
	 Week = 2,
	 Stop = "Complete timecard for this week",
	 Complete = true,
	 Spartan = nish
 },
 new Tracker
 {
	 Week = 3,
	 Stop = "Complete timecard for this week",
	 Complete = true,
	 Spartan = nish
 },
 new Tracker
 {
	 Week = 4,
	 Stop = "Complete timecard for this week",
	 Complete = true,
	 Spartan = nish



 },
 new Tracker
 {
	 Week = 1,
	 Stop = "Complete timecard for this week",
	 Complete = true,
	 Spartan = peter
 }
 );
			context.SaveChanges();
		}
	}
}
