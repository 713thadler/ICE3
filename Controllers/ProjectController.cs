using Microsoft.AspNetCore.Mvc;
using lab1.Data;
using lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Controllers
{
    public class ProjectController : Controller
    {
        private readonly Lab1Context _context;

        public ProjectController(Lab1Context context)
        {
            _context = context;
        }

        // GET: Projects - Retrieves all projects and returns them to the view.
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();

            return View(projects);
        }

        // GET: Projects/Details/5 - Retrieves details of a specific project.
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _context.Projects
                .Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create - Returns the create project view.
        public IActionResult Create()
        {
            return View(new Project { Name = "", Description = "", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, EndedAt = null, Status = "" }); // Initialize an empty project to be used in the Create view
        }

        // POST: Projects/Create - Creates a new project.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description,CreatedAt,UpdatedAt,EndedAt,Status")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5 - Retrieves a project for editing.
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5 - Saves the edited project.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("ProjectId,Name,Description,CreatedAt,UpdatedAt,EndedAt,Status")] Project project)
        {

            if (ModelState.IsValid)
            {

                _context.Update(project);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5 - Retrieves a project for deletion confirmation.
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var project = _context.Projects
                .FirstOrDefault(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5 - Deletes a project.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
                TempData["Message"] = "Project deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
