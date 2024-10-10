namespace TicketsPanel.Services.Implementations
{
    public class EmailSettings
    {
        string Server { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
