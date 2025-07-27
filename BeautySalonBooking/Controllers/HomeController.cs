using Microsoft.AspNetCore.Mvc;
using BeautySalonBooking.Data;        // for _context
using BeautySalonBooking.Models;      // for Booking
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BeautySalonBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Book()
        {
            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Book(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ThankYou));
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
