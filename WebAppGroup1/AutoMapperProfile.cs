using AutoMapper;
using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;

namespace WebAppGroup1
{
	public class AutoMapperProfile : Profile
	{
        public AutoMapperProfile()
        {
            CreateMap<Tracker, TrackerEditVM>().ReverseMap();
            CreateMap<Tracker, TrackerCreateVM>().ReverseMap();
            CreateMap<Tracker, TrackerVM>().ReverseMap();
            CreateMap<Tracker, TrackerDetailsVM>().ReverseMap();
        }
    }

}
