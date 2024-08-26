namespace TicketsPanel.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public char Situation { get; set; } 

        public int ManagerId { get; set; }
        public User Manager { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        
        public ICollection<User> Users { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
