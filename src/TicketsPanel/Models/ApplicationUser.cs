using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace TicketsPanel.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public char? Situation { get; set; } = 'A';
        public int DepartmentId { get; set; }
        public string Position { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        public string? InactivatedAt { get; set; } = null;
        public string? UserInactiveAction { get; set; } = null;


        public Department? Department { get; set; }

        public ICollection<Ticket>? TicketsClient { get; set; }
        public ICollection<Ticket>? TicketsAttendant { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
