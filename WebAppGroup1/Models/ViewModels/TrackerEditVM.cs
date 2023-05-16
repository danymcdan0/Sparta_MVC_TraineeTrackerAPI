using System.ComponentModel.DataAnnotations;

namespace WebAppGroup1.Models.ViewModels
{
    public class TrackerEditVM
    {
        [Required(ErrorMessage = "Week is required")]
        [Range(1, 8)]
        public int Id { get; set; }
        public int Week { get; set; }
        public string? Stop { get; set; }
        public string? Start { get; set; }
        public string? Continue { get; set; }
        public string? Comments { get; set; }

        [Required(ErrorMessage = "Technical Skill is required")]
        [Display(Name = "Technical Skill [1-4]")]
        [Range(1, 4)]
        public int TechnicalSkill { get; set; }

        [Required(ErrorMessage = "Soft Skill is required")]
        [Display(Name = "Soft Skill [1-4]")]
        [Range(1, 4)]
        public int SoftSkill { get; set; }
        public Spartan? Spartan { get; set; }
    }
}
