using TicketsPanel.Enums;

namespace TicketsPanel.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public int PriotiryId { get; set; }
        public ICollection<string>? Emails { get; set; }
        public ICollection<string>? Attachment { get; set; }
        public int? AttendantId { get; set; }
        public string Situation { get; set; } = "No defined attendant";
        public bool ReceiveResponse { get; set; } = true;
        public bool SendReply { get; set; } = true;
        public DateTime OpenTime { get; set; } = DateTime.UtcNow;
        public DateTime? CloseTime { get; set; } = null;
        public DateTime Sla { get; set; }

        public Department Department { get; set; }
        public Category Category { get; set; }
        public Priority Priority { get; set; }
        public User Attendant { get; set; } = null; 

        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Clients { get; set; }



    }
}
