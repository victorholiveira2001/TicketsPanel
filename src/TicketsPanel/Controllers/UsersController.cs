using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsPanel.Data;
using TicketsPanel.Models;
using TicketsPanel.Services;

namespace TicketsPanel.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher _passwordHasher;

        public UsersController(ApplicationDbContext context, PasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [Route("usuario")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Users.Include(u => u.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("usuario/detalhes/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Route("usuario/criar")]
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("usuario/criar")]
        public async Task<IActionResult> Create([Bind("UserId,Name,SSN,Email,Password,Phone,DepartmentId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = _passwordHasher.SetPassword(user.Password);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", user.DepartmentId);
            return View(user);
        }

        [Route("usuario/editar/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("usuairo/editar/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Name,SSN,Email,Password,Phone,DepartmentId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            var userTmp = await _context.Users.FindAsync(id);

            user.Password = userTmp.Password;

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Users.FindAsync(id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    _context.Entry(existingUser).CurrentValues.SetValues(user);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", user.DepartmentId);
            return View(user);
        }

        [Route("usuario/excluir/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Department)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Situation != 'A')
            {
                TempData["AlertMessage"] = "O usuário solicitado já encontra-se inativo.";
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("usuario/excluir/{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                user.Situation = 'I';
                _context.Users.Update(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
