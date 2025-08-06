using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautySalonBooking.Data;
using BeautySalonBooking.Models;
using System.Threading.Tasks;

namespace BeautySalonBooking.Controllers
{
    [Authorize] // kräver inloggning
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Service/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Service/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home"); // t.ex. tillbaka till startsidan
            }

            return View(service);
        }
    }
}
