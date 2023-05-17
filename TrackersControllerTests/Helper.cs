using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;

namespace WebAppTests;

public static class Helper
{
	public static ServiceResponse<Spartan> GetSpartanServiceResponse()
	{
		var response = new ServiceResponse<Spartan>();
		response.Data = new Spartan
		{
			Id = "Id",
			Email = "Nooreen@spartaglobal.com",
			EmailConfirmed = true
		};

		return response;
	}

	public static ServiceResponse<T> GetFailedServiceResponse<T>(string message = "")
	{
		var response = new ServiceResponse<T>();
		response.Success = false;
		response.Message = message;
		return response;
	}

	public static ServiceResponse<IEnumerable<TrackerVM>> GetToDoListServiceResponse()
	{
		var response = new ServiceResponse<IEnumerable<TrackerVM>>();
		response.Data = new List<TrackerVM>
			{
				Mock.Of<TrackerVM>(),
				Mock.Of<TrackerVM>()
			};
		return response;
	}

	public static ServiceResponse<TrackerDetailsVM> GetDetailsServiceResponse()
	{
		var response = new ServiceResponse<TrackerDetailsVM>();
		response.Data = Mock.Of<TrackerDetailsVM>();
		return response;
	}
	public static ServiceResponse<TrackerEditVM> GetEditDetailsServiceResponse()
	{
		var response = new ServiceResponse<TrackerEditVM>();
		response.Data = Mock.Of<TrackerEditVM>();
		return response;
	}
	public static ServiceResponse<MarkCompleteVM> GetMarkCompleteServiceResponse()
	{
		var response = new ServiceResponse<MarkCompleteVM>();
		response.Data = Mock.Of<MarkCompleteVM>();
		return response;
	}
	
	public static ServiceResponse<TrackerCreateVM> GetCreateServiceResponse()
	{
		var response = new ServiceResponse<TrackerCreateVM>();
		response.Data = Mock.Of<TrackerCreateVM>();
		return response;
	}

	public static ServiceResponse<TrackerVM> GetServiceResponse()
	{
		var response = new ServiceResponse<TrackerVM>();
		response.Data = Mock.Of<TrackerVM>();
		return response;
	}
}
