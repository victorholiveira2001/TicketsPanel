namespace PainelDeChamado.Models{
    public class Message{
        public int MessageId { get; set; }
        public string Body { get; set; }
        public DateTimeOffset SentTime { get; set; }
        public int SenderId { get; set; }
        public int TicketId { get; set; }
        
        public User Sender { get; set; }
        public Ticket Ticket { get; set; }
    }
}