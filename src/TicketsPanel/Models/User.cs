using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketsPanel.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; } //Nosso CPF aqui no Brasil
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public char? Situation { get; set; } = 'A';
        public int DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? InactivatedAt { get; set; } = null;
        public string? UserInactiveAction { get; set; } = null;


        public Department Department { get; set; }

        public ICollection<Role>? Roles { get; set; }
        public ICollection<Ticket>? TicketsClient { get; set; }
        public ICollection<Ticket>? TicketsAttendant { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
