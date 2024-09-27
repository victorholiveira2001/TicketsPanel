using TicketsPanel.Enums;

namespace TicketsPanel.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public List<string>? Emails { get; set; }
        public List<string>? Attachment { get; set; }
        public int? AttendantId { get; set; }
        public Situation Situation { get; set; }
        public bool ReceiveResponse { get; set; } = true;
        public bool SendReply { get; set; } = true;
        public string OpenTime { get; set; }
        public DateTime? CloseTime { get; set; } = null;
        public DateTime Sla { get; set; }

        public Department Department { get; set; }
        public Category Category { get; set; }
        public Priority Priority { get; set; }
        public ApplicationUser Attendant { get; set; } = null; 

        public ICollection<Message> Messages { get; set; }
        public ICollection<ApplicationUser> Clients { get; set; }



    }
}
