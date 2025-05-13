using EventEaseBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EventEaseBookingSystem.Controllers
{
    public class VenueController : Controller
    {
        private readonly AppDbContext _context;
        private readonly BlobService _blobService;

        public VenueController(AppDbContext context, BlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Venue model, IFormFile image)
        {
            try
            {
                if (!ModelState.IsValid || image == null)
                {
                    TempData["Error"] = "All fields and an image are required!";
                    return View(model);
                }

                if (model.Capacity <= 0)
                {
                    TempData["Error"] = "Capacity must be a positive number!";
                    return View(model);
                }

                // Upload image to Azure Blob Storage
                string fileName = $"venues/{Guid.NewGuid()}_{image.FileName}";
                using var stream = image.OpenReadStream();
                string imageUrl = await _blobService.UploadFileAsync(stream, fileName);

                model.ImageUrl = imageUrl;
                _context.Venues.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Venue added successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var venue = await _context.Venues.FindAsync(id);
                if (venue == null)
                {
                    TempData["Error"] = "Venue not found!";
                    return RedirectToAction("Index");
                }

                if (_context.Bookings.Any(b => b.VenueId == id))
                {
                    TempData["Error"] = "Cannot delete venue with active bookings!";
                    return RedirectToAction("Index");
                }

                string fileName = venue.ImageUrl.Split('/').Last();
                _blobService.DeleteFile(fileName);

                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Venue deleted successfully!";
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
            var venues = await _context.Venues.ToListAsync();
            return View(venues);
        }
    }
}