using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppGroup1.Models.ViewModels
{
	public class TrackerVM
	{
        public string? Owner { get; set; }
        public int Id { get; set; }
		public int Week { get; set; }

		[Display(Name = "Technical Skill")]
		public string TechnicalSkill { get; set; } = null!;

		[Display(Name = "Soft Skill")]
		public string SoftSkill { get; set; } = null!;

		public bool Complete { get; set; }

	}
}
