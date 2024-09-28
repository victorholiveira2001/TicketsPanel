using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketsPanel.Enums;

namespace TicketsPanel.Models
{
    public class Ticket
    {
        [Display(Name = "Protocolo")]
        public int TicketId { get; set; }
        [Display(Name = "Assunto")]
        public string Title { get; set; }
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public List<string>? Emails { get; set; }
        public List<string>? Attachment { get; set; }
        public int ClientId { get; set; }
        public int? AttendantId { get; set; }

        [Display(Name = "Situação")]
        public Situation Situation { get; set; }
        public bool ReceiveResponse { get; set; } = true;
        public bool SendReply { get; set; } = true;
        public string OpenTime { get; set; }
        public DateTime? CloseTime { get; set; } = null;
        public DateTime Sla { get; set; }

        [Display(Name = "Departamento")]
        public Department Department { get; set; }
        public Category Category { get; set; }
        public Priority Priority { get; set; }

        [Display(Name = "Cliente")]
        public ApplicationUser Client { get; set; }

        public ApplicationUser Attendant { get; set; } = null;

        [Display(Name = "Mensagem")]
        public ICollection<Message> Messages { get; set; } = new List<Message>();


        [NotMapped]
        public Message NewMessage { get; set; }

    }
}
