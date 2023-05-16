using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;

namespace WebAppGroup1.Services
{
	public interface ITrackerService
	{
		Task<IEnumerable<TrackerVM>> GetTrackerEntriesAsync(Spartan? spartan);
		Task<TrackerDetailsVM> GetDetailsAsync(Spartan? spartan, int? id);
		Task<TrackerCreateVM> CreateToDoAsync(Spartan? spartan, TrackerCreateVM createTodoVM);
		Task<TrackerDetailsVM> EditToDoAsync(Spartan? spartan, int? id, TrackerVM todoVM);
		Task<TrackerVM> UpdateToDoCompleteAsync(Spartan? spartan, int id, MarkCompleteVM markCompleteVM);
		Task<TrackerVM> DeleteToDoAsync(Spartan? spartan, int? id);
		Task<Spartan> GetUserAsync(HttpContext httpContext);
		Task<string> GetSpartanOwnerAsync(int? id);
		bool ToDoExists(int id);
		string GetRole(HttpContext httpContext);
	}
}
