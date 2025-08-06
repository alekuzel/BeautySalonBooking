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

        public IActionResult Create(int serviceId)
{
    var service = _context.Services.FirstOrDefault(s => s.Id == serviceId);
    if (service == null)
    {
        return NotFound();
    }

    var booking = new Booking
    {
        ServiceId = serviceId,
        Service = service,
        Date = DateTime.Today.AddDays(1) // 👈 Set default to tomorrow
    };

    ViewData["Services"] = new SelectList(_context.Services, "Id", "Name", serviceId);
    return View(booking);
}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction("ThankYou", "Home");
            }

            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name", booking.ServiceId);
            return View(booking);
        }
    }
}