using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppGroup1.Models.ViewModels
{
	public class TrackerVM
	{
		public int Id { get; set; }
		public int Week { get; set; }

		[Display(Name = "Technical Skill [1-4]")]
		public int TechnicalSkill { get; set; }

		[Display(Name = "Soft Skill [1-4]")]
		public int SoftSkill { get; set; }

		public bool Complete { get; set; }

	}
}
