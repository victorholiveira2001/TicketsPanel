using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsPanel.Data;
using TicketsPanel.Models;
using TicketsPanel.Services;

namespace TicketsPanel.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserService _userService;

        public DepartmentsController(ApplicationDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [Route("departamento")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Departments.Include(d => d.Manager).Include(d => d.Organization);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("departamento/detalhes/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                TempData["AlertMessage"] = "O departamento solicitado não existe, tente novamente.";
                return RedirectToAction("Index");
            }

            var department = await _context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Organization)
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [Route("departamento/criar")]
        public IActionResult Create()
        {
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Name");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("departamento/criar")]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name,ManagerId,OrganizationId")] Department department)
        {

            department.Manager = _context.Users.Find(department.ManagerId);
            department.Organization = _context.Organizations.Find(department.OrganizationId);

            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();

                var user = _context.Users.Find(department.ManagerId);

                await _userService.UpdateManagerId(department.DepartmentId, user);

                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Name", department.ManagerId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", department.OrganizationId);
            return View(department);
        }

        [Route("departamento/editar/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Name", department.ManagerId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "Name", department.OrganizationId);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("departamento/editar/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name,Situation,ManagerId,OrganizationId")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _context.Users.Find(department.ManagerId);

                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    await _userService.UpdateManagerId(department.DepartmentId, user);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Name", department.ManagerId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", department.OrganizationId);
            return View(department);
        }

        [Route("departamento/excluir/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Organization)
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("departamento/excluir/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.DepartmentId == id);
        }
    }
}
