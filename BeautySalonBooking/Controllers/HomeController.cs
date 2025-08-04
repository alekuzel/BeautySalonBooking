using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using BeautySalonBooking.Data;
using BeautySalonBooking.Models;

namespace BeautySalonBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Book
        public IActionResult Book()
        {
            ViewData["Services"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: /Home/Book
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: /Home/ThankYou
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
