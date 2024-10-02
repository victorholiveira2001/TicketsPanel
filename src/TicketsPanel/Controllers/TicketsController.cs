using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketsPanel.Data;
using TicketsPanel.Enums;
using TicketsPanel.Models;

namespace TicketsPanel.Controllers
{
    //[Authorize]
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
            var applicationDbContext = _context.Tickets.Include(t => t.Client).Include(t => t.Attendant).Include(t => t.Category).Include(t => t.Department);
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
                .Include(t => t.Messages)
                    .ThenInclude(m => m.Sender)
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
            var ticket = _context.Tickets.Include(t => t.Messages);


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
        public async Task<IActionResult> Create(Ticket ticket)//[Bind("TicketId,Title,DepartmentId,CategoryId,PriotiryId,Emails,Attachment,AttendantId,Situation,ReceiveResponse,SendReply,OpenTime,CloseTime,Sla,Priority")] Ticket ticket)
        {

            if (ticket.NewMessage != null)
            {
                ticket.NewMessage.SentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                ticket.NewMessage.SenderId = 1;
                ticket.Messages.Add(ticket.NewMessage);
            }
               
            
            ticket.Client =  await _userManager.GetUserAsync(User);
            ticket.ClientId = ticket.Client.Id;

            //ticket.ClientId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
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

        public async Task<IActionResult> Take(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            var attendant = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            ticket.AttendantId = attendant.Id;
            ticket.Situation = Situation.WaitingForAttendent;

            _context.Update(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction($"Detalhes", "Chamado", new { id = ticket.TicketId });
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
