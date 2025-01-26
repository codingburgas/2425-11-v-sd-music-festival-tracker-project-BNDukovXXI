namespace Program.Models
{
    public class MusicalEvent
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int AvailableTickets { get; set; }
    }
}