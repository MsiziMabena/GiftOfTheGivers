using GiftOfTheGivers.Data;
using GiftOfTheGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Controllers
{
    [Authorize]
    public class IncidentReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IncidentReportsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show all incidents (public to logged in users)
        public async Task<IActionResult> Index()
        {
            var reports = await _context.IncidentReports
                .Include(r => r.ReportedByUser)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();
            return View(reports);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentReport model)
        {
            ModelState.Remove("ReportedByUserId");
            ModelState.Remove("ReportedByUser");

            if (!ModelState.IsValid) return View(model);

            model.ReportedByUserId = _userManager.GetUserId(User);
            model.ReportDate = DateTime.UtcNow;

            _context.IncidentReports.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var report = await _context.IncidentReports.Include(r => r.ReportedByUser).FirstOrDefaultAsync(r => r.ReportId == id);
            if (report == null) return NotFound();
            return View(report);
        }

        // Edit/Delete can be added later; we've kept minimal CRUD for speed
    }
}
