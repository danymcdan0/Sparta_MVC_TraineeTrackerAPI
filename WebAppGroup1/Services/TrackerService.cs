using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var response = new ServiceResponse<TrackerVM>();

            if (spartan == null)
            {
                response.Succcess = false;
                response.Message = "No user found";
                return response;
            }
            try
            {
                var toDo = _mapper.Map<ToDo>(createTodoVM);
                toDo.Spartan = spartan;
                _context.Add(toDo);
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

        public Task<ServiceResponse<TrackerVM>> DeleteTrackerEntriesAsync(Spartan? spartan, int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TrackerDetailsVM>> EditTrackerEntriesAsync(Spartan? spartan, int? id, TrackerVM todoVM)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TrackerDetailsVM>> GetDetailsAsync(Spartan? spartan, int? id)
        {
            throw new NotImplementedException();
        }

        public string GetRole(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSpartanOwnerAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<TrackerVM>>> GetTrackerEntriesAsync(Spartan? spartan)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<Spartan>> GetUserAsync(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public bool TrackerEntriesExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TrackerVM>> UpdateTrackerEntriesCompleteAsync(Spartan? spartan, int id, MarkCompleteVM markCompleteVM)
        {
            throw new NotImplementedException();
        }
    }
}
