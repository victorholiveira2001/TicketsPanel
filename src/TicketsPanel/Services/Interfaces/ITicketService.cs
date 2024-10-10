using System.Security.Claims;
using TicketsPanel.Models;

namespace TicketsPanel.Services.Interfaces
{
    public interface ITicketService
    {
        public Task<bool> SendMessageAsync(int ticketId, string body, ClaimsPrincipal user);
        public Task<bool> FinishTicketAsync(int ticketId, string body, ClaimsPrincipal user);
    }
}
