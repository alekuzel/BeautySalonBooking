using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BeautySalonBooking.Data;
using BeautySalonBooking.Models;
using System.Threading.Tasks;

namespace BeautySalonBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking/Create
        public IActionResult Create(int? serviceId)
        {
            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name", serviceId);
            var booking = new Booking
            {
                ServiceId = serviceId ?? 0,
                DateTime = DateTime.Now.AddDays(1) // default date
            };
            return View(booking);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThankYou");
            }

            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name", booking.ServiceId);
            return View(booking);
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
