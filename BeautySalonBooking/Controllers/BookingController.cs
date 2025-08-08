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

        // GET: Booking/Create?serviceId=1
        // GET: Booking/Create?serviceId=1
public IActionResult Create(int? serviceId)
{
    if (serviceId.HasValue)
    {
        var service = _context.Services.FirstOrDefault(s => s.Id == serviceId.Value);
        if (service == null)
        {
            return NotFound();
        }

        var booking = new Booking
        {
            ServiceId = service.Id,
            Date = DateTime.Now.AddDays(1)
        };

        ViewData["Service"] = service;
        return View(booking);
    }

    // This is the part likely broken or skipped
    var services = _context.Services.ToList(); // Fetch the data
    ViewData["Services"] = new SelectList(services, "Id", "ServiceName"); // Not "Name"
    
    var newBooking = new Booking
    {
        Date = DateTime.Now.AddDays(1)
    };
    return View(newBooking);
}


     [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Booking booking)
{
    if (ModelState.IsValid)
    {
        Console.WriteLine($"Booking for {booking.FirstName} - ServiceId: {booking.ServiceId} - Date: {booking.Date}");
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return RedirectToAction("ThankYou");
    }

    Console.WriteLine("Model is NOT valid!");
    foreach (var modelState in ViewData.ModelState.Values)
    {
        foreach (var error in modelState.Errors)
        {
            Console.WriteLine(error.ErrorMessage);
        }
    }

    return View(booking);
}


        public IActionResult ThankYou()
        {
            return View();
        }

        // GET: Admin/Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
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
    }
}
