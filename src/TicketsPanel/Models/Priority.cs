namespace PainelDeChamado.Models
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string Name { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}