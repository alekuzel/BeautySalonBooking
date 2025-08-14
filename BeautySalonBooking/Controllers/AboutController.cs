using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautySalonBooking.Data;
using BeautySalonBooking.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonBooking.Controllers
{
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AboutController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Public About page
        [AllowAnonymous]
        public IActionResult Index()
        {
            var about = _context.AboutPages.FirstOrDefault();
            if (about == null)
            {
                // If no content exists, show placeholder
                about = new AboutPage { Title = "Om oss", Content = "Information kommer snart." };
            }
            return View(about);
        }

        // Admin edit
        [Authorize]
        public IActionResult Edit()
        {
            var about = _context.AboutPages.FirstOrDefault();
            if (about == null)
            {
                about = new AboutPage { Title = "Om oss" };
                _context.AboutPages.Add(about);
                _context.SaveChanges();
            }
            return View(about);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AboutPage model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
