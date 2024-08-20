namespace PainelDeChamado.Models{
    public class Organization{
        public int OrganizationId { get; set; }
        public string Name { get; set; }

        public string? Email { get;set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}