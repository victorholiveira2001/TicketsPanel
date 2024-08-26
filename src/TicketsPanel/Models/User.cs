namespace TicketsPanel.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; } //Nosso CPF aqui no Brasil
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<Ticket> TicketsClient { get; set; }
        public ICollection<Ticket> TicketsAttendant { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
