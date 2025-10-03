using GiftOfTheGivers.Data;
using GiftOfTheGivers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Controllers
{
    [Authorize]
    public class VolunteerAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VolunteerAssignmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: VolunteerAssignments
        public async Task<IActionResult> Index()
        {
            var assignments = _context.VolunteerAssignments
                .Include(a => a.VolunteerTask)
                .Include(a => a.VolunteerUser);

            return View(await assignments.ToListAsync());
        }

        // POST: VolunteerAssignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int taskId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var task = await _context.VolunteerTasks.FindAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            // Prevent duplicate sign-ups
            bool alreadyAssigned = await _context.VolunteerAssignments
                .AnyAsync(a => a.TaskId == taskId && a.VolunteerUserId == user.Id);

            if (alreadyAssigned)
            {
                TempData["Message"] = "You have already signed up for this task.";
                return RedirectToAction("Details", "VolunteerTasks", new { id = taskId });
            }

            var assignment = new VolunteerAssignment
            {
                TaskId = taskId,
                VolunteerUserId = user.Id,
                AssignmentDate = DateTime.Now
            };

            _context.VolunteerAssignments.Add(assignment);
            await _context.SaveChangesAsync();

            TempData["Message"] = "You have successfully signed up for the task!";
            return RedirectToAction("Index");
        }
    }
}
