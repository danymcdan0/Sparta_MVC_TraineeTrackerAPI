﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppGroup1.Data;
using WebAppGroup1.Models;

namespace WebAppGroup1.Controllers
{
    public class TrackersController : Controller
    {
        private readonly SpartaTrackerContext _context;

        public TrackersController(SpartaTrackerContext context)
        {
            _context = context;
        }

        // GET: Trackers
        public async Task<IActionResult> Index()
        {
            var spartaTrackerContext = _context.TrackerEntries.Include(t => t.Spartan);
            return View(await spartaTrackerContext.ToListAsync());
        }

        // GET: Trackers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrackerEntries == null)
            {
                return NotFound();
            }

            var tracker = await _context.TrackerEntries
                .Include(t => t.Spartan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tracker == null)
            {
                return NotFound();
            }

            return View(tracker);
        }

        // GET: Trackers/Create
        public IActionResult Create()
        {
            ViewData["SpartanId"] = new SelectList(_context.Spartans, "Id", "Id");
            return View();
        }

        // POST: Trackers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Week,Stop,Start,Continue,Comments,TechnicalSkill,SoftSkill,Complete,SpartanId")] Tracker tracker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tracker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpartanId"] = new SelectList(_context.Spartans, "Id", "Id", tracker.SpartanId);
            return View(tracker);
        }

        // GET: Trackers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrackerEntries == null)
            {
                return NotFound();
            }

            var tracker = await _context.TrackerEntries.FindAsync(id);
            if (tracker == null)
            {
                return NotFound();
            }
            ViewData["SpartanId"] = new SelectList(_context.Spartans, "Id", "Id", tracker.SpartanId);
            return View(tracker);
        }

        // POST: Trackers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Week,Stop,Start,Continue,Comments,TechnicalSkill,SoftSkill,Complete,SpartanId")] Tracker tracker)
        {
            if (id != tracker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tracker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackerExists(tracker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpartanId"] = new SelectList(_context.Spartans, "Id", "Id", tracker.SpartanId);
            return View(tracker);
        }

        // GET: Trackers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrackerEntries == null)
            {
                return NotFound();
            }

            var tracker = await _context.TrackerEntries
                .Include(t => t.Spartan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tracker == null)
            {
                return NotFound();
            }

            return View(tracker);
        }

        // POST: Trackers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrackerEntries == null)
            {
                return Problem("Entity set 'SpartaTrackerContext.TrackerEntries'  is null.");
            }
            var tracker = await _context.TrackerEntries.FindAsync(id);
            if (tracker != null)
            {
                _context.TrackerEntries.Remove(tracker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrackerExists(int id)
        {
          return (_context.TrackerEntries?.Any(e => e.Id == id)).GetValueOrDefault();
        }

		//UpdateTrackerComplete
	}
}
