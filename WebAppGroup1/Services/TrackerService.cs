using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAppGroup1.Data;
using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;

namespace WebAppGroup1.Services
{
    public class TrackerService : ITrackerService
    {
        private SpartaTrackerContext _context;
        private IMapper _mapper;
        private UserManager<Spartan> _userManager;

        public TrackerService(SpartaTrackerContext context, IMapper mapper, UserManager<Spartan> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<TrackerCreateVM>> CreateTrackerEntriesAsync(Spartan? spartan, TrackerCreateVM trackerCreateVM)
        {
            var response = new ServiceResponse<TrackerCreateVM>();

            if (spartan == null)
            {
                response.Success = false;
                response.Message = "No user found";
                return response;
            }
            try
            {
                var trackerToDo = _mapper.Map<Tracker>(trackerCreateVM);
                trackerToDo.Spartan = spartan;
				trackerToDo.Owner = spartan.UserName;
                _context.Add(trackerToDo);
                await _context.SaveChangesAsync();
                return response;
            }
            catch
            {
                response.Success = false;
                response.Message = "Database could not be updated";
            }
            return response;
        }

        public async Task<ServiceResponse<TrackerVM>> DeleteTrackerEntriesAsync(Spartan? spartan, int? id)
        {
            var response = new ServiceResponse<TrackerVM>();

            var trackerToDo = await _context.TrackerEntries.FindAsync(id);

            if (spartan == null)
            {
                response.Success = false;
                response.Message = "No user found";
                return response;
            }
            if (trackerToDo == null)
            {
                response.Success = false;
                response.Message = "There are no tracker entries to do!";
                return response;
            }

            if (trackerToDo.SpartanId == spartan.Id)
            {
                _context.TrackerEntries.Remove(trackerToDo);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Tracker entry removed";
            }
            return response;
        }

        public async Task<ServiceResponse<TrackerEditVM>> EditTrackerEntriesAsync(Spartan? spartan, int? id, TrackerEditVM trackerEditVM)
        {

            var response = new ServiceResponse<TrackerEditVM>();

            if (spartan == null)
            {
                response.Success = false;
                response.Message = "No user found";
                return response;
            }

            var spartanOwnerId = await GetSpartanOwnerAsync(id);
            if (id != trackerEditVM.Id)
            {
                response.Message = "Error updating";
                response.Success = false;
                return response;
            }

            var trackerToDo = _mapper.Map<Tracker>(trackerEditVM);
            trackerToDo.Owner = spartan.UserName;
            _context.Update(trackerToDo);
            trackerToDo.SpartanId = spartan.Id;
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<ServiceResponse<TrackerDetailsVM>> GetDetailsAsync(Spartan? spartan, int? id, string role)
        {
            var response = new ServiceResponse<TrackerDetailsVM>();
            if (id == null || _context.TrackerEntries == null)
            {
                response.Success = false;
                return response;
            }

            if (spartan == null)
            {
                response.Success = false;
                response.Message = "No user found";
                return response;
            }

            var trackerToDo = await _context.TrackerEntries
                .FirstOrDefaultAsync(m => m.Id == id);

            if (role == "Trainer")
            {
                response.Data = _mapper.Map<TrackerDetailsVM>(trackerToDo);
                return response;
			}

            if (trackerToDo == null || (trackerToDo.SpartanId != spartan.Id))
            {
                response.Success = false;
                return response;
            }

            response.Data = _mapper.Map<TrackerDetailsVM>(trackerToDo);
            return response;

        }

        public async Task<ServiceResponse<TrackerEditVM>> GetEditDetailsAsync(Spartan? spartan, int? id)
        {
            var response = new ServiceResponse<TrackerEditVM>();
            if (id == null || _context.TrackerEntries == null)
            {
                response.Success = false;
                return response;
            }

            if (spartan == null)
            {
                response.Success = false;
                response.Message = "No user found";
                return response;
            }

            var trackerToDo = await _context.TrackerEntries
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trackerToDo == null || (trackerToDo.SpartanId != spartan.Id))
            {
                response.Success = false;
                return response;
            }

            response.Data = _mapper.Map<TrackerEditVM>(trackerToDo);
            return response;

        }

        public string GetRole(HttpContext httpContext)
        {
            return httpContext.User.IsInRole("Trainee") ? "Trainee" : "Trainer";
        }

        public async Task<string> GetSpartanOwnerAsync(int? id)
        {
            return await _context.TrackerEntries.Where(td => td.Id == id).Select(td => td.SpartanId).FirstAsync();
        }

        public async Task<ServiceResponse<IEnumerable<TrackerVM>>> GetTrackerEntriesAsync(Spartan? spartan, string role, string filter)
        {
            var response = new ServiceResponse<IEnumerable<TrackerVM>>();
            if (spartan == null)
            {
                response.Success = false;
                response.Message = "Can't find Spartan";
                return response;
            }
            if (_context.TrackerEntries == null)
            {
                response.Success = false;
                response.Message = "There are no tracker entries to do!";
                return response;
            }

            List<Tracker> trackers = new List<Tracker>();

            if (role == "Trainee")
            {
                // if the role is trainee
                // get the todo itemers
                // where the SpartanId of that todo item = the Id of the spartan
                trackers = await _context.TrackerEntries.Where(td => td.SpartanId == spartan.Id).ToListAsync();
            }
            if (role == "Trainer")
            {
                // Trainer can see all the to dos!!
                trackers = await _context.TrackerEntries.ToListAsync();
            }

            if (string.IsNullOrEmpty(filter))
            {
                response.Data = trackers.Select(td => _mapper.Map<TrackerVM>(td));
                return response;
            };

            //trackers = await _context.TrackerEntries.Where(td => td.SpartanId == spartan.Id).ToListAsync();
            response.Data = trackers
                .Where(td =>
                    td.Owner.Contains(filter!, StringComparison.OrdinalIgnoreCase)) /*||
                    td.SoftSkill.Contains(filter!, StringComparison.OrdinalIgnoreCase) ||
					td.TechnicalSkill.Contains(filter!, StringComparison.OrdinalIgnoreCase))*/
                .Select(t => _mapper.Map<TrackerVM>(t));

            

            return response;
        }

        public async Task<ServiceResponse<Spartan>> GetUserAsync(HttpContext httpContext)
        {
            var response = new ServiceResponse<Spartan>();

            var currentUser = await _userManager.GetUserAsync(httpContext.User);

            if (currentUser == null)
            {
                response.Success = false;
                response.Message = "Could not find Spartan";
                return response;
            }

            response.Data = currentUser;
            return response;
        }

        public bool TrackerEntriesExists(int id)
        {
            return (_context.TrackerEntries?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<ServiceResponse<TrackerVM>> UpdateTrackerEntriesCompleteAsync(Spartan? spartan, int id, MarkCompleteVM markCompleteVM, string role = "Trainee")
        {
            var response = new ServiceResponse<TrackerVM>();
            if (spartan == null)
            {
                response.Success = false;
                response.Message = "No user found";
                return response;
            }
            if (id != markCompleteVM.Id)
            {
                response.Success = false;
                response.Message = "Model error";
                return response;
            }
            if (role == "Trainee")
            {
				response.Success = true;
				return response;
			}

            var trackerToDo = await _context.TrackerEntries.FindAsync(id);

            if (trackerToDo == null)
            {
                response.Success = false;
                response.Message = "Cannot find tracker entry";
                return response;
            }

            trackerToDo.Complete = markCompleteVM.Complete;

            await _context.SaveChangesAsync();
            return response;
        }

    }
}
