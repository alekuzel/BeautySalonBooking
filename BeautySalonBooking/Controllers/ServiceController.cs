using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BeautySalonBooking.Data;
using BeautySalonBooking.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonBooking.Controllers
{
    [Authorize] // Require login for admin actions by default
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Service (Public view)
        [AllowAnonymous]
        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        // GET: /Service/Create (Admin only)
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
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: /Service/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();

            return View(service);
        }


        // GET: /Service/Edit/5
        public IActionResult Edit(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();

            return View(service);
        }

        // POST: /Service/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (id != service.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        [Authorize] // Only logged in users can manage
public IActionResult Manage()
{
    var services = _context.Services.ToList();
    return View(services);
}


        // GET: /Service/Delete/5
        public IActionResult Delete(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);
            if (service == null)
                return NotFound();

            return View(service);
        }

        

        // POST: /Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
