using BeautySalonBooking.Data;
using BeautySalonBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeautySalonBooking.Controllers.Admin
{
    [Authorize] // Ensure only logged-in users access
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Booking
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.Include(b => b.Service).ToListAsync();

            return View(bookings);
        }

[HttpGet]
[AllowAnonymous]
public IActionResult Create(int serviceId, DateTime? date)
{
    var service = _context.Services.FirstOrDefault(s => s.Id == serviceId);
    if (service == null)
    {
        return NotFound();
    }

    var model = new Booking
    {
        ServiceId = serviceId,
        Date = date ?? DateTime.Now
    };

    ViewData["Service"] = service;
    return View(model);
}


        [HttpPost]
       [AllowAnonymous]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState errors:");
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Field: {key} - Error: {error.ErrorMessage}");
                    }
                }

                var service = _context.Services.FirstOrDefault(s => s.Id == booking.ServiceId);
                ViewData["Service"] = service;
                return View(booking);
            }

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("ThankYou");
        }


        [AllowAnonymous]

        public IActionResult ThankYou()
        {
            return View();
        }

        // GET: Admin/Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Service) // Load the related Service
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null) return NotFound();

            ViewData["Services"] = new SelectList(_context.Services, "Id", "ServiceName", booking.ServiceId);
            return View(booking);
        }


        // POST: Admin/Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Services"] = new SelectList(_context.Services, "Id", "ServiceName", booking.ServiceId);
            return View(booking);
        }

        // GET: Admin/Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Service)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // POST: Admin/Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
         [AllowAnonymous]

public IActionResult WeekView(int serviceId, int weekOffset = 0)
{
    var service = _context.Services.FirstOrDefault(s => s.Id == serviceId);
    if (service == null)
        return NotFound();

    var today = DateTime.Today.AddDays(weekOffset * 7);
    int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
    var weekStart = today.AddDays(-diff);
    var weekDays = Enumerable.Range(0, 7).Select(i => weekStart.AddDays(i)).ToList();

    var bookings = _context.Bookings
        .Where(b => b.ServiceId == serviceId &&
                    b.Date.Date >= weekStart &&
                    b.Date.Date < weekStart.AddDays(7))
        .ToList();

    var model = new WeekViewModel
    {
        ServiceId = service.Id,
        ServiceName = service.ServiceName,
        WeekDays = weekDays,
        Bookings = bookings,
        WeekOffset = weekOffset
    };

    return View(model);
}



    }
}
