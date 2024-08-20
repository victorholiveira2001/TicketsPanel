namespace PainelDeChamado.Models
{
   public class Category
   {
      public int CategoryId { get; set; }
      public string Name { get; set; }
      public int DepartmentId { get; set; }
      public Department Department { get; set; }
      public ICollection<Ticket> Tickets { get; set; }
   }
}