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

			var peter = new Spartan
			{
				UserName = "Pbellaby",
				Email = "PBellaby@spartaglobal.com",
				EmailConfirmed = true,
			};
			var phil = new Spartan
			{
				UserName = "Pthomas",
				Email = "PThomas@spartaglobal.com",
				EmailConfirmed = true,
			};
			var danyal = new Spartan
			{
				UserName = "Dsaleh",
				Email = "DSaleh@spartaglobal.com",
				EmailConfirmed = true
			};
			var daniel = new Spartan
			{
				UserName = "Dmanu",
				Email = "DManu@spartaglobal.com",
				EmailConfirmed = true
			};
			var matt = new Spartan
			{
				UserName = "Mhandley",
				Email = "MHandley@spartaglobal.com",
				EmailConfirmed = true
			};
			var idris = new Spartan
			{
				UserName = "Aidris",
				Email = "AIdris@spartaglobal.com",
				EmailConfirmed = true
			};
			var nooreen = new Spartan
			{
				UserName = "Nali",
				Email = "NAli@spartaglobal.com",
				EmailConfirmed = true
			};


			userManager
			 .CreateAsync(phil, "Password1!")
			 .GetAwaiter()
			 .GetResult();
			userManager
			.CreateAsync(peter, "Password1!")
			.GetAwaiter()
			.GetResult();
			userManager
			.CreateAsync(danyal, "Password1!")
			.GetAwaiter()
			.GetResult();
			userManager
			.CreateAsync(daniel, "Password1!")
			.GetAwaiter()
			.GetResult();
			userManager
			.CreateAsync(matt, "Password1!")
			.GetAwaiter()
			.GetResult();
			userManager
			.CreateAsync(idris, "Password1!")
			.GetAwaiter()
			.GetResult();
			userManager
			.CreateAsync(nooreen, "Password1!")
			.GetAwaiter()
			.GetResult();

			context.UserRoles.AddRange(new IdentityUserRole<string>[]
{
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(peter).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainer).Result
			 },
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(phil).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 },
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(danyal).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 },
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(daniel).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 },
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(matt).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 },
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(idris).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 },
			 new IdentityUserRole<string>
			 {
			 UserId = userManager.GetUserIdAsync(nooreen).Result,
			 RoleId = roleStore.GetRoleIdAsync(trainee).Result
			 }


			 });

			context.TrackerEntries.AddRange(
 new Tracker
 {
	 Week = 1,
	 Start = "Complete the weekly survey",
	 Complete = false,
	 Spartan = nooreen,
	 Comments = "lorem",
	 Owner = "Nali",
	 SoftSkill = "Partially Skilled",
	 TechnicalSkill = "Partially Skilled"
 },
 new Tracker
 {
	 Week = 2,
	 Stop = "Complete timecard for this week",
	 Complete = true,
	 Comments = "ipsum",
	 Spartan = idris,
	 Owner = "Aidris",
	 SoftSkill = "Unskilled",
	 TechnicalSkill = "Partially Skilled"
 },
 new Tracker
 {
	 Week = 3,
	 Stop = "Complete timecard for this week",
	 Complete = true,
	 Spartan = matt,
	 Owner = "Mhandley",
	 SoftSkill = "Low Skilled",
	 TechnicalSkill = "Partially Skilled"
 },
 new Tracker
 {
	 Week = 4,
	 Stop = "Complete timecard for this week",
	 Complete = true,
	 Spartan = daniel,
	 Comments = "dolor",
	 Owner = "Dmanu",
	 SoftSkill = "Skilled",
	 TechnicalSkill = "Skilled"



 },
  new Tracker
  {
	  Week = 8,
	  Stop = "Complete timecard for this week",
	  Start = "Taking breaks",
	  Complete = true,
	  Spartan = phil,
	  Owner = "Pthomas",
	  SoftSkill = "Skilled",
	  TechnicalSkill = "Skilled"



  },
 new Tracker
 {
	Week = 1,
	Stop = "Complete timecard for this week",
	Complete = true,
	Spartan = danyal,
	Owner = "Dsaleh",
	SoftSkill = "Partially Skilled",
	TechnicalSkill = "Partially Skilled"
 }
 );
			context.SaveChanges();
		}
	}
}
