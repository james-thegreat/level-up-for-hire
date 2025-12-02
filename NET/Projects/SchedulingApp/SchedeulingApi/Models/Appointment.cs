namespace SchedulingApi.Models
{
    // Represents one appointment in the schedule
    public class Appointment
    {
        public int Id { get; set; }          // Unique ID
        public string Title { get; set; }    // Description (e.g. "Dentist")
        public DateTime StartTime { get; set; } // When it starts
    }
}
