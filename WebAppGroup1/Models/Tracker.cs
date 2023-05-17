using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppGroup1.Models
{
	public class Tracker
	{
		[Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Week is required")]
        [MaxLength(1)]
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
        public bool Complete { get; set; } = false;
        public Spartan? Spartan { get; set; }

        [ValidateNever]
        [ForeignKey("Spartan")]
        public string SpartanId { get; set; } = null!;

		public string? Owner { get; set; }
	}
}
