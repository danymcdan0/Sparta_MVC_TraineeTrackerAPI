using Microsoft.AspNetCore.Identity;
using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;

namespace WebAppGroup1.Services
{
	public interface ITrackerService
	{
		Task<ServiceResponse<IEnumerable<TrackerVM>>> GetTrackerEntriesAsync(Spartan? spartan, string role = "Trainee", string filter = null);
		Task<ServiceResponse<TrackerDetailsVM>> GetDetailsAsync(Spartan? spartan, int? id, string role = "Trainee");
		Task<ServiceResponse<TrackerCreateVM>> CreateTrackerEntriesAsync(Spartan? spartan, TrackerCreateVM trackerCreateVM);
		Task<ServiceResponse<TrackerEditVM>> EditTrackerEntriesAsync(Spartan? spartan, int? id, TrackerEditVM trackerDetailsVM);
		Task<ServiceResponse<TrackerVM>> UpdateTrackerEntriesCompleteAsync(Spartan? spartan, int id, MarkCompleteVM markCompleteVM, string role = "Trainee");
		Task<ServiceResponse<TrackerVM>> DeleteTrackerEntriesAsync(Spartan? spartan, int? id);
		Task<ServiceResponse<TrackerEditVM>> GetEditDetailsAsync(Spartan? spartan, int? id);
        Task<ServiceResponse<Spartan>> GetUserAsync(HttpContext httpContext);
		Task<string> GetSpartanOwnerAsync(int? id);
		bool TrackerEntriesExists(int id);
		string GetRole(HttpContext httpContext);
	}
}
