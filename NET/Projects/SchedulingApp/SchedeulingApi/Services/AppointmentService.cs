using SchedulingApi.Models;

namespace SchedulingApi.Services
{
    // Acts like a mini in-memory database of appointments
    public class AppointmentService
    {
        private readonly List<Appointment> _appointments = new();
        private int _nextId = 1; // Used to assign new IDs

        public AppointmentService()
        {
            // Seed with a sample appointment
            _appointments.Add(new Appointment
            {
                Id = _nextId++,
                Title = "Example: Meet with James",
                StartTime = DateTime.Now.AddHours(2)
            });
        }

        public List<Appointment> GetAll()
        {
            return _appointments;
        }

        public Appointment Add(Appointment newAppointment)
        {
            newAppointment.Id = _nextId++;
            _appointments.Add(newAppointment);
            return newAppointment;
        }
    }
}
