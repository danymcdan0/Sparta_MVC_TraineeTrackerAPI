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

		[Display(Name = "Technical Skill [1-4]")]
		public int TechnicalSkill { get; set; }

		[Display(Name = "Soft Skill [1-4]")]
		public int SoftSkill { get; set; }
		public Spartan? Spartan { get; set; }
		public string SpartanId { get; set; } = null!;
		public string? Owner { get; set; }
	}
}
