using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NuGet.Protocol;
using WebAppGroup1.Controllers;
using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;
using WebAppGroup1.Services;
using WebAppTests;

namespace TrackersControllerTests;

public class Tests
{
	private TrackersController? _sut;

	[Test]
	public void BeAbleTobeConstructed()
	{
		var mockService = new Mock<ITrackerService>();
		_sut = new TrackersController(mockService.Object);
		Assert.That(_sut, Is.InstanceOf<TrackersController>());
	}

	[Test]
	public void Index_WithSuccessfulServiceResponse_ReturnsVMList()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
				   .Returns(spartanServiceResponse);
		mockService.Setup(s => s.GetTrackerEntriesAsync(spartanServiceResponse.Data, It.IsAny<string>(), It.IsAny<string>()))
					.ReturnsAsync(Helper.GetToDoListServiceResponse());
		mockService.Setup(s => s.GetRole(It.IsAny<HttpContext>()))
					.Returns("Trainer");

		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Index().Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ViewResult>());

		var viewResult = result as ViewResult;
		var data = viewResult!.Model;
		Assert.That(data, Is.InstanceOf<IEnumerable<TrackerVM>>());
	}


	[Test]
	public void Index_WithUnsuccessfulServiceResponse_ReturnsProblem()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();

		var failedResponse = Helper.GetFailedServiceResponse<IEnumerable<TrackerVM>>("Fake problem message");

		mockService.Setup(s => s.GetTrackerEntriesAsync(It.IsAny<Spartan>(), It.IsAny<string>(), It.IsAny<string>()).Result)
				   .Returns(failedResponse);
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
					.Returns(spartanServiceResponse);

		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Index().Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ObjectResult>());

		var objectResult = result as ObjectResult;


		Assert.That(result.ToJson(), Does.Contain("Fake problem"));
		Assert.That((int)objectResult!.StatusCode, Is.EqualTo(500));
	}


	[Test]
	public void Edit_WithSuccessfulServiceResponse_ReturnsRedirectToAction()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();

		var response = Helper.GetEditDetailsServiceResponse();

		mockService.Setup(s => s.EditTrackerEntriesAsync(It.IsAny<Spartan>(), It.IsAny<int>(), It.IsAny<TrackerEditVM>()).Result)
				   .Returns(response);
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
					.Returns(spartanServiceResponse);
		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Edit(It.IsAny<int>(), It.IsAny<TrackerEditVM>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

		var objectResult = result as RedirectToActionResult;


		var redirectToActionResult = result as RedirectToActionResult;
		Assert.That(redirectToActionResult!.ActionName, Is.EqualTo("Index"));

	}

	[Test]
	public void Edit_WithUnsuccessfulServiceResponse_ReturnsRedirectToAction()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var failedResponse = Helper.GetFailedServiceResponse<TrackerEditVM>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();
		failedResponse.Message = "Sad";
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
				.Returns(spartanServiceResponse);
		mockService.Setup(s => s.EditTrackerEntriesAsync(It.IsAny<Spartan>(), It.IsAny<int>(), It.IsAny<TrackerEditVM>()).Result)
				   .Returns(failedResponse);

		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Edit(It.IsAny<int>(), It.IsAny<TrackerEditVM>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ObjectResult>());


		var objectResult = result as ObjectResult;
		Assert.That(objectResult.ToJson(), Does.Contain("Sad"));
		Assert.That((int)objectResult!.StatusCode, Is.EqualTo(500));
	}

	[Test]
	public void Create_WithSuccessfulServiceResponse_ReturnsRedirectToAction()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();

		var response = Helper.GetCreateServiceResponse();

		mockService.Setup(s => s.CreateTrackerEntriesAsync(It.IsAny<Spartan>(), It.IsAny<TrackerCreateVM>()).Result)
				   .Returns(response);
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
					.Returns(spartanServiceResponse);
		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Create(It.IsAny<TrackerCreateVM>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

		var objectResult = result as RedirectToActionResult;


		var redirectToActionResult = result as RedirectToActionResult;
		Assert.That(redirectToActionResult!.ActionName, Is.EqualTo("Index"));

	}

	[Test]
	public void Create_WithUnsuccessfulServiceResponse_ReturnsRedirectToAction()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var failedResponse = Helper.GetFailedServiceResponse<TrackerCreateVM>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();
		failedResponse.Message = "Sad";
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
				.Returns(spartanServiceResponse);
		mockService.Setup(s => s.CreateTrackerEntriesAsync(It.IsAny<Spartan>(), It.IsAny<TrackerCreateVM>()).Result)
				   .Returns(failedResponse);

		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Create(It.IsAny<TrackerCreateVM>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ViewResult>());


		//var objectResult = result as ViewResult;
		//Assert.That(objectResult.ToJson(), Does.Contain("Sad"));
		//Assert.That((int)objectResult!.StatusCode, Is.EqualTo(500));
	}

	[Test]
	public void Details_WithSuccessfulServiceResponse_ReturnsView()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();

		var response = Helper.GetDetailsServiceResponse();

		mockService.Setup(s => s.GetDetailsAsync(It.IsAny<Spartan>(), It.IsAny<int>(), It.IsAny<string>()).Result)
				   .Returns(response);
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
					.Returns(spartanServiceResponse);
		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Details(It.IsAny<int>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ViewResult>());
	}

	[Test]
	public void Details_WithUnsuccessfulServiceResponse_ReturnsProblem()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var failedResponse = Helper.GetFailedServiceResponse<TrackerDetailsVM>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();
		failedResponse.Message = "Sad";
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
				.Returns(spartanServiceResponse);
		mockService.Setup(s => s.GetDetailsAsync(It.IsAny<Spartan>(), It.IsAny<int>(), It.IsAny<string>()).Result)
				   .Returns(failedResponse);

		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Details(It.IsAny<int>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ObjectResult>());


		var objectResult = result as ObjectResult;
		Assert.That(objectResult.ToJson(), Does.Contain("Sad"));
		Assert.That((int)objectResult!.StatusCode, Is.EqualTo(500));
	}

	[Test]
	public void Delete_WithSuccessfulServiceResponse_ReturnsRedirectToAction()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();

		var response = Helper.GetServiceResponse();

		mockService.Setup(s => s.DeleteTrackerEntriesAsync(It.IsAny<Spartan>(), It.IsAny<int>()).Result)
				   .Returns(response);
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
					.Returns(spartanServiceResponse);
		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Delete(It.IsAny<int>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

		var objectResult = result as RedirectToActionResult;


		var redirectToActionResult = result as RedirectToActionResult;
		Assert.That(redirectToActionResult!.ActionName, Is.EqualTo("Index"));

	}

	[Test]
	public void Delete_WithUnsuccessfulServiceResponse_ReturnsProblem()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var failedResponse = Helper.GetFailedServiceResponse<TrackerVM>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();
		failedResponse.Message = "Sad";
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
				.Returns(spartanServiceResponse);
		mockService.Setup(s => s.DeleteTrackerEntriesAsync(It.IsAny<Spartan>(), It.IsAny<int>()).Result)
				   .Returns(failedResponse);

		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.Delete(It.IsAny<int>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ObjectResult>());


		var objectResult = result as ObjectResult;
		Assert.That(objectResult.ToJson(), Does.Contain("Sad"));
		Assert.That((int)objectResult!.StatusCode, Is.EqualTo(500));
	}

	[Test]
	public void UpdateTraineeTrackerComplete_WithSuccessfulServiceResponse_ReturnsRedirectToAction()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();

		var response = Helper.GetServiceResponse();

		mockService.Setup(s => s.UpdateTrackerEntriesCompleteAsync(It.IsAny<Spartan>(), It.IsAny<int>(), It.IsAny<MarkCompleteVM>(), It.IsAny<string>()).Result)
				   .Returns(response);
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
					.Returns(spartanServiceResponse);
		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.UpdateTraineeTrackerComplete(It.IsAny<int>(), It.IsAny<MarkCompleteVM>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<RedirectToActionResult>());

		var objectResult = result as RedirectToActionResult;


		var redirectToActionResult = result as RedirectToActionResult;
		Assert.That(redirectToActionResult!.ActionName, Is.EqualTo("Index"));

	}

	[Test]
	public void UpdateTraineeTrackerComplete_WithUnsuccessfulServiceResponse_ReturnsProblem()
	{
		// Arrange
		var mockService = new Mock<ITrackerService>();
		var failedResponse = Helper.GetFailedServiceResponse<TrackerVM>();
		var spartanServiceResponse = Helper.GetSpartanServiceResponse();
		failedResponse.Message = "Sad";
		mockService.Setup(s => s.GetUserAsync(It.IsAny<HttpContext>()).Result)
				.Returns(spartanServiceResponse);
		mockService.Setup(s => s.UpdateTrackerEntriesCompleteAsync(It.IsAny<Spartan>(), It.IsAny<int>(), It.IsAny<MarkCompleteVM>(), It.IsAny<string>()).Result)
				   .Returns(failedResponse);

		_sut = new TrackersController(mockService.Object);

		// Act
		var result = _sut.UpdateTraineeTrackerComplete(It.IsAny<int>(), It.IsAny<MarkCompleteVM>()).Result;

		// Assert
		Assert.That(result, Is.InstanceOf<ObjectResult>());


		var objectResult = result as ObjectResult;
		Assert.That(objectResult.ToJson(), Does.Contain("Sad"));
		Assert.That((int)objectResult!.StatusCode, Is.EqualTo(500));
	}
}