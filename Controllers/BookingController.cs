using EventEaseBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventEaseBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Venues = _context.Venues.ToList();
            ViewBag.Events = _context.Events.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                if (!ModelState.IsValid || booking.VenueId == 0 || booking.EventId == 0)
                {
                    TempData["Error"] = "All fields are required!";
                    ViewBag.Venues = _context.Venues.ToList();
                    ViewBag.Events = _context.Events.ToList();
                    return View(booking);
                }

                if (endTime <= startTime)
                {
                    TempData["Error"] = "End time must be after start time!";
                    ViewBag.Venues = _context.Venues.ToList();
                    ViewBag.Events = _context.Events.ToList();
                    return View(booking);
                }

                var eventItem = await _context.Events.FindAsync(booking.EventId);
                if (eventItem == null)
                {
                    TempData["Error"] = "Selected event not found!";
                    ViewBag.Venues = _context.Venues.ToList();
                    ViewBag.Events = _context.Events.ToList();
                    return View(booking);
                }

                bool isDoubleBooked = await _context.Bookings
                    .Join(_context.Events,
                        b => b.EventId,
                        e => e.EventId,
                        (b, e) => new { Booking = b, Event = e })
                    .Where(be => be.Booking.VenueId == booking.VenueId
                        && be.Event.EventDate.Date == booking.BookingDate.Date
                        && (
                            (be.Event.StartTime <= startTime && be.Event.EndTime >= startTime) ||
                            (be.Event.StartTime <= endTime && be.Event.EndTime >= endTime) ||
                            (be.Event.StartTime >= startTime && be.Event.EndTime <= endTime)
                        ))
                    .AnyAsync();

                if (isDoubleBooked)
                {
                    TempData["Error"] = "This venue is already booked for the selected time slot!";
                    ViewBag.Venues = _context.Venues.ToList();
                    ViewBag.Events = _context.Events.ToList();
                    return View(booking);
                }

                eventItem.StartTime = startTime;
                eventItem.EndTime = endTime;
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Booking added successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                ViewBag.Venues = _context.Venues.ToList();
                ViewBag.Events = _context.Events.ToList();
                return View(booking);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);
                if (booking == null)
                {
                    TempData["Error"] = "Booking not found!";
                    return RedirectToAction("Index");
                }

                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Booking deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            var query = _context.BookingViews.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(b => b.BookingId.ToString().Contains(searchQuery)
                    || b.EventName.Contains(searchQuery));
            }

            var bookings = await query.ToListAsync();
            return View(bookings);
        }
    }
}
