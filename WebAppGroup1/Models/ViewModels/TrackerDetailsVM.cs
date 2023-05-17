using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppGroup1.Models.ViewModels
{
	public class TrackerDetailsVM
	{
		public int Id { get; set; }
		public int Week { get; set; }
		public string? Stop { get; set; }
		public string? Start { get; set; }
		public string? Continue { get; set; }
		public string? Comments { get; set; }

		[Display(Name = "Technical Skill")]
		public string TechnicalSkill { get; set; } = null!;

		[Display(Name = "Soft Skill")]
		public string SoftSkill { get; set; } = null!;
		public Spartan? Spartan { get; set; }
		public string SpartanId { get; set; } = null!;
		public string? Owner { get; set; }
	}
}
