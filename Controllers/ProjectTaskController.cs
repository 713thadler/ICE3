using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab1.Models;
using lab1.Data;
using System.Threading.Tasks;
using System.Linq;

namespace lab1
{
    public class ProjectTaskController : Controller
    {
        private readonly Lab1Context _context;

        public ProjectTaskController(Lab1Context context)
        {
            _context = context;
        }

        // Display all tasks for a specific project
        public async Task<IActionResult> Index(int projectId)
        {
            // Ensure you return a list of ProjectTasks for a specific project
            var projectTasks = await _context.ProjectTasks
                                             .Where(pt => pt.ProjectId == projectId)
                                             .Include(pt => pt.Project)
                                             .ToListAsync();

            ViewBag.ProjectId = projectId; // Optionally pass projectId to the view for back navigation or header information
            return View(projectTasks);
        }

        // Details of a specific task
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound("Task ID not provided.");
            }

            var task = await _context.ProjectTasks
                                     .Include(t => t.Project)
                                     .FirstOrDefaultAsync(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return View(task);
        }

        // Create a new task form
        public IActionResult Create(int projectId)
        {
            // Initialize your new ProjectTask with the ProjectId and ProjectName to bind it automatically in the form
            var newTask = new ProjectTask
            {
                ProjectId = projectId,
                ProjectName = "" // Add the ProjectName property here
            };
            return View(newTask);
        }

        // POST: Create a new task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ProjectId,Status,StartDate,EndDate")] ProjectTask taskModel)
        {
            if (ModelState.IsValid)
            {
                _ = _context.Add(taskModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = taskModel.ProjectId });
            }
            return View(taskModel);
        }

        // Edit task form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound("Task ID not provided.");
            }

            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return View(task);
        }

        // POST: Edit a task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId,Title,Description,ProjectId,Status,StartDate,EndDate")] ProjectTask taskModel)
        {
            if (id != taskModel.ProjectTaskId)
            {
                return NotFound("Mismatched Task ID.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(taskModel.ProjectTaskId))
                    {
                        return NotFound($"Task with ID {id} not found.");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { projectId = taskModel.ProjectId });
            }
            return View(taskModel);
        }

        // Confirm deletion of a task
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound("Task ID not provided.");
            }

            var task = await _context.ProjectTasks
                                     .Include(t => t.Project)
                                     .FirstOrDefaultAsync(t => t.ProjectTaskId == id);

            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return View(task);
        }

        // POST: Delete a task after confirmation
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = task.ProjectId });
            }
            return NotFound($"Task with ID {id} not found.");
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.ProjectTaskId == id);
        }
    }
}
