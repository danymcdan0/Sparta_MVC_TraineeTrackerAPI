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

	public static ServiceResponse<TrackerVM> GetToDoItemServiceResponse()
	{
		var response = new ServiceResponse<TrackerVM>();
		response.Data = Mock.Of<TrackerVM>();
		return response;
	}
}
