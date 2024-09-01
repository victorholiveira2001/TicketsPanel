namespace TicketsPanel.Models
{
   public class Category
   {
        public Category()
        {
            Tickets = new List<Ticket>() { };
            CreatedAt = DateTime.UtcNow;
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
   }
}