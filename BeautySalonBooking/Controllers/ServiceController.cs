using Microsoft.AspNetCore.Mvc;
using BeautySalonBooking.Data;
using System.Linq;

namespace BeautySalonBooking.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }
    }
}
