using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsPanel.Data;
using TicketsPanel.Models;

namespace TicketsPanel.Controllers
{
    public class CategoriesController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("categoria")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("categoria/criar")]
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("categoria/criar")]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,DepartmentId,CreatedAt")] Category category)
        {
            if (ModelState.IsValid) 
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", category.DepartmentId);
                return View();
            }
            return View(category);
        }
    }
}
