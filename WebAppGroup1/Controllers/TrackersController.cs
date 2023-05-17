using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppGroup1.Data;
using WebAppGroup1.Models;
using WebAppGroup1.Models.ViewModels;
using WebAppGroup1.Services;

namespace WebAppGroup1.Controllers
{
    [Authorize]
    public class TrackersController : Controller
    {
        private readonly ITrackerService _service;

        public TrackersController(ITrackerService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Trainee, Trainer")]
        // GET: Trackers
        public async Task<IActionResult> Index(string filter = null)
        {
            var user = await _service.GetUserAsync(HttpContext);
            var response = await _service.GetTrackerEntriesAsync(user.Data, _service.GetRole(HttpContext), filter);

            return response.Success ? View(response.Data) : Problem(response.Message);
        }

        [Authorize(Roles = "Trainee, Trainer")]
        // GET: Trackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var currentUser = await _service.GetUserAsync(HttpContext);
            var response = await _service.GetDetailsAsync(currentUser.Data, id, _service.GetRole(HttpContext));
            return response.Success? View(response.Data) : Problem(response.Message);

        }

        // GET: Trackers/Create
        [Authorize(Roles = "Trainee")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainee")]

        public async Task<IActionResult> Create(TrackerCreateVM trackerCreateVM)
        {

            var currentUser = await _service.GetUserAsync(HttpContext);
            var response = await _service.CreateTrackerEntriesAsync(currentUser.Data, trackerCreateVM);
            return response.Success ? RedirectToAction(nameof(Index)) : View(trackerCreateVM);
        }

        // GET: Trackers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainee")]
        public async Task<IActionResult> Edit(int id, TrackerEditVM trackerEditVM)
        {
            var currentUser = await _service.GetUserAsync(HttpContext);
            var response = await _service.EditTrackerEntriesAsync(currentUser.Data, id, trackerEditVM);
            return response.Success ? RedirectToAction(nameof(Index)) : Problem(response.Message);
        }

        // POST: Trackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpGet]
        [Authorize(Roles = "Trainee")]

        public async Task<IActionResult> Edit(int? id)
        {
            var currentUser = await _service.GetUserAsync(HttpContext);
            var response = await _service.GetEditDetailsAsync(currentUser.Data, id);
            return response.Success ? View(response.Data) : NotFound();
        }

        // GET: Trackers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainee")]

        public async Task<IActionResult> Delete(int? id)
        {

            var currentUser = await _service.GetUserAsync(HttpContext);
            var response = await _service.DeleteTrackerEntriesAsync(currentUser.Data, id);
            return response.Success ? RedirectToAction(nameof(Index)) : Problem(response.Message);
        }

        //UpdateTrackerComplete

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Trainer")]

        public async Task<IActionResult> UpdateTraineeTrackerComplete(int id, MarkCompleteVM markCompleteVM)
        {
            var currentUser = await _service.GetUserAsync(HttpContext);
            var response = await _service.UpdateTrackerEntriesCompleteAsync(currentUser.Data, id, markCompleteVM, _service.GetRole(HttpContext));
            return response.Success ? RedirectToAction(nameof(Index)) : Problem(response.Message);

        }
    }


}
