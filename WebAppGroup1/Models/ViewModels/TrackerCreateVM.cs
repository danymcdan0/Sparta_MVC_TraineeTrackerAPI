using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAppGroup1.Models.ViewModels
{
	public class TrackerCreateVM
	{
        [Required(ErrorMessage = "Week is required")]
        [Range(1,8)]
        public int Week { get; set; }
		public string? Stop { get; set; }
		public string? Start { get; set; }
		public string? Continue { get; set; }
		public string? Comments { get; set; }

        [Required(ErrorMessage = "Technical Skill is required")]
        [Display(Name = "Technical Skill")]
        public string TechnicalSkill { get; set; } = null!;

        [Required(ErrorMessage = "Soft Skill is required")]
        [Display(Name = "Soft Skill")]
        public string SoftSkill { get; set; } = null!;

        [HiddenInput(DisplayValue = false)]
		public string? Email { get; set; }
	}
}
