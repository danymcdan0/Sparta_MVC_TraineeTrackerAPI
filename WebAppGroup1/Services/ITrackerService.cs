using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;

namespace WebAppGroup1.Services
{
	public interface ITrackerService
	{
		Task<ServiceResponse<IEnumerable<TrackerVM>>> GetTrackerEntriesAsync(Spartan? spartan);
		Task<ServiceResponse<TrackerDetailsVM>> GetDetailsAsync(Spartan? spartan, int? id);
		Task<ServiceResponse<TrackerCreateVM>> CreateTrackerEntriesAsync(Spartan? spartan, TrackerCreateVM trackerCreateVM);
		Task<ServiceResponse<TrackerDetailsVM>> EditTrackerEntriesAsync(Spartan? spartan, int? id, TrackerVM trackerVM);
		Task<ServiceResponse<TrackerVM>> UpdateTrackerEntriesCompleteAsync(Spartan? spartan, int id, MarkCompleteVM markCompleteVM);
		Task<ServiceResponse<TrackerVM>> DeleteTrackerEntriesAsync(Spartan? spartan, int? id);
		Task<ServiceResponse<Spartan>> GetUserAsync(HttpContext httpContext);
		Task<string> GetSpartanOwnerAsync(int? id);
		bool TrackerEntriesExists(int id);
		string GetRole(HttpContext httpContext);
	}
}
