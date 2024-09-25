using System.ComponentModel.DataAnnotations;

namespace TicketsPanel.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public char Situation { get; set; } = 'A';
        [Display(Name = "Gestor")]
        public int ManagerId { get; set; }
        public ApplicationUser? Manager { get; set; }
        [Display(Name = "Organização")]
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        
        public ICollection<ApplicationUser>? Users { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

    }
}
