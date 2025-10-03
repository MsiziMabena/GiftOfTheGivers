using GiftOfTheGivers.Data;
using GiftOfTheGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Controllers
{
    [Authorize] // Allows all logged-in users to view tasks
    public class VolunteerTasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VolunteerTasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /VolunteerTasks
        public async Task<IActionResult> Index()
        {
            // Include assignments for each task
            var tasks = await _context.VolunteerTasks
                                      .Include(t => t.Assignments)
                                      .ToListAsync();
            return View(tasks);
        }

        // GET: /VolunteerTasks/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var task = await _context.VolunteerTasks
                                     .Include(t => t.Assignments)
                                     .FirstOrDefaultAsync(t => t.TaskId == id);
            if (task == null) return NotFound();
            return View(task);
        }

        // GET: /VolunteerTasks/Create
        [Authorize(Roles = "Admin")] // Only admins can create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /VolunteerTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only admins can create
        public async Task<IActionResult> Create(VolunteerTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }
    }
}
