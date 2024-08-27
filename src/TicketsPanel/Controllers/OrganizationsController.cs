using Microsoft.AspNetCore.Mvc;
using TicketsPanel.Data;
using TicketsPanel.Models;

namespace TicketsPanel.Controllers
{
    public class OrganizationsController : Controller
    {

        private readonly ApplicationDbContext _context;
        public OrganizationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("organizacao/")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("organizacao/criar")]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("organizacao/criar")]
        public async Task<IActionResult> Create(Organization organization)
        {
            if (ModelState.IsValid) 
            {
                await _context.AddAsync(organization);

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(organization);
        }
    }
}
