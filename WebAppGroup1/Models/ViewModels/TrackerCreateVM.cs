using System.ComponentModel.DataAnnotations;

namespace WebAppGroup1.Models.ViewModels
{
	public class TrackerCreateVM
	{
		public int Week { get; set; }
		public string? Stop { get; set; }
		public string? Start { get; set; }
		public string? Continue { get; set; }
		public string? Comments { get; set; }

		[Display(Name = "Technical Skill [1-4]")]
		public int TechnicalSkill { get; set; }

		[Display(Name = "Soft Skill [1-4]")]
		public int SoftSkill { get; set; }
	}
}
