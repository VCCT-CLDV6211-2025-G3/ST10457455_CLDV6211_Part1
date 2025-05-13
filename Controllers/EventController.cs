using EventEaseBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EventEaseBookingSystem.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Venues = _context.Venues.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "All fields are required!";
                    ViewBag.Venues = _context.Venues.ToList();
                    return View(model);
                }

                if (model.EndTime <= model.StartTime)
                {
                    TempData["Error"] = "End time must be after start time!";
                    ViewBag.Venues = _context.Venues.ToList();
                    return View(model);
                }

                _context.Events.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Event added successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                ViewBag.Venues = _context.Venues.ToList();
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var eventItem = await _context.Events.FindAsync(id);
                if (eventItem == null)
                {
                    TempData["Error"] = "Event not found!";
                    return RedirectToAction("Index");
                }

                if (_context.Bookings.Any(b => b.EventId == id))
                {
                    TempData["Error"] = "Cannot delete event with active bookings!";
                    return RedirectToAction("Index");
                }

                _context.Events.Remove(eventItem);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Event deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.Include(e => e.Venue).ToListAsync();
            return View(events);
        }
    }
}