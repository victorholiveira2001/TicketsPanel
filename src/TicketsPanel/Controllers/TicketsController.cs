using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsPanel.Data;
using TicketsPanel.Models;

namespace TicketsPanel.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public TicketsController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets
        [Route("Chamado")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tickets.Include(t => t.Attendant).Include(t => t.Category).Include(t => t.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        [Route("Chamado/Detalhes/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Attendant)
                .Include(t => t.Category)
                .Include(t => t.Department)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Route("Chamado/Criar")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chamado/Criar")]
        public async Task<IActionResult> Create([Bind("TicketId,Title,DepartmentId,CategoryId,PriotiryId,Emails,Attachment,AttendantId,Situation,ReceiveResponse,SendReply,OpenTime,CloseTime,Sla,Priority")] Ticket ticket)
        {
            ticket.Clients = new List<ApplicationUser>() { await _userManager.GetUserAsync(User) };
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", ticket.CategoryId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", ticket.DepartmentId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Route("Chamado/Editar/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["AttendantId"] = new SelectList(_context.Users, "UserId", "Name", ticket.AttendantId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", ticket.CategoryId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", ticket.DepartmentId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Chamedo/Editar/{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,Title,DepartmentId,CategoryId,PriotiryId,Emails,Attachment,AttendantId,Situation,ReceiveResponse,SendReply,OpenTime,CloseTime,Sla,Priority")] Ticket ticket)
        {
            if (id != ticket.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketId))
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
            ViewData["AttendantId"] = new SelectList(_context.Users, "UserId", "Name", ticket.AttendantId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", ticket.CategoryId);
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", ticket.DepartmentId);
            return View(ticket);
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketId == id);
        }
    }
}
