using Microsoft.AspNetCore.Identity;
using TicketsPanel.Models;
using TicketsPanel.Data;
using System.Security.Claims;
using TicketsPanel.Enums;
using TicketsPanel.Services.Interfaces;

namespace TicketsPanel.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> SendMessageAsync(int ticketId, string body, ClaimsPrincipal user)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);

            if (ticket == null)
            {
                return false;
            }

            var sender = await _userManager.GetUserAsync(user);
            if (sender == null)
            {
                return false;
            }

            ticket.Messages.Add(new Message
            {
                Body = body,
                SenderId = int.Parse(_userManager.GetUserId(user)),
                SentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                TicketId = ticket.TicketId
            });


            _context.Update(ticket);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> FinishTicketAsync(int ticketId, string body, ClaimsPrincipal user)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

            bool result = await SendMessageAsync(ticketId, body, user);
            if (result) 
            {
                ticket.Situation = Situation.Finished;
                ticket.CloseTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                _context.Update(ticket);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
