using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PainelDeChamado.Models;
using TicketsPanel.Data;
using TicketsPanel.Services;

namespace TicketsPanel.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher _passswordHaser;

        public UserController(ApplicationDbContext context, PasswordHasher passwordHasher)
        {
            _context = context;
            _passswordHaser = passwordHasher;
        }

        [HttpGet]
        [Route("User/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model, string password)
        {
            if (ModelState.IsValid)
            {
                _passswordHaser.SetPassword(password);
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        [Route("TicketPanel/Login")]
        public IActionResult Login(){
            return View();
        }

        public async Task<IActionResult> Login(string email, string password){
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user != null && _passswordHaser.VerifyPassword(password))
            {
                return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("","Invalid username or password");
            return View();
        }
    }
}