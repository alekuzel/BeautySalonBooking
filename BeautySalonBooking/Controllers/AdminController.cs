using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautySalonBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Controllers
{
    [Authorize] // optional: protect the admin area
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Bookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Service)
                .ToListAsync();

            return View(bookings);
        }
    }
}
