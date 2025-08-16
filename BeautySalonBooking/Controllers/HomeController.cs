using Microsoft.AspNetCore.Mvc;
using BeautySalonBooking.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var services = await _context.Services.ToListAsync();
        return View(services);
    }

    public IActionResult About()
    {
        return View();
    }
    
    public IActionResult Backdoor()
{
    ViewData["ShowAuthLinks"] = true;
    return View();
}


}
