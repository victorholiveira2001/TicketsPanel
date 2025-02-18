﻿using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Anexos")]
        public List<string>? Attachment { get; set; }
        public int ClientId { get; set; }
        public int? AttendantId { get; set; }

        [Display(Name = "Situação")]
        public Situation Situation { get; set; }
        public bool ReceiveResponse { get; set; } = true;
        public bool SendReply { get; set; } = true;

        [Display(Name = "Data de abertura")]
        public string OpenTime { get; set; } = DateTime.Now.ToString();
        public string? CloseTime { get; set; } = null;

        [Display(Name = "SLA")]
        public DateTime? Sla { get; set; } = null;

        [Display(Name = "Departamento")]
        public Department? Department { get; set; }

        [Display(Name = "Categoria")]
        public Category? Category { get; set; }

        [Display(Name = "Prioridade")]
        public Priority Priority { get; set; }

        [Display(Name = "Cliente")]
        public ApplicationUser? Client { get; set; }


        [Display(Name = "Atendente")]
        public ApplicationUser? Attendant { get; set; } = null;

        [Display(Name = "Mensagem")]
        public ICollection<Message>? Messages { get; set; } = new List<Message>();
    }
}
