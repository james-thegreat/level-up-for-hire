using SchedulingApi.Models; // or SchedeulingApi.Models depending on your namespace

namespace SchedulingApi.Services
{
    public class AppointmentService
    {
        private readonly List<Appointment> _appointments = new();
        private int _nextId = 1;

        public AppointmentService()
        {
            _appointments.Add(new Appointment
            {
                Id = _nextId++,
                Title = "Example: Meet with James",
                StartTime = DateTime.Now.AddHours(2)
            });
        }

        // Get all appointments
        public List<Appointment> GetAll()
        {
            return _appointments;
        }

        // Get one appointment by id
        public Appointment? GetById(int id)
        {
            return _appointments.FirstOrDefault(a => a.Id == id);
        }

        // Add a new appointment
        public Appointment Add(Appointment newAppointment)
        {
            newAppointment.Id = _nextId++;
            _appointments.Add(newAppointment);
            return newAppointment;
        }

        // Update an existing appointment
        public bool Update(int id, Appointment updated)
        {
            var existing = _appointments.FirstOrDefault(a => a.Id == id);
            if (existing == null)
            {
                return false;
            }

            // Overwrite fields
            existing.Title = updated.Title;
            existing.StartTime = updated.StartTime;

            return true;
        }

        // Delete an appointment
        public bool Delete(int id)
        {
            var existing = _appointments.FirstOrDefault(a => a.Id == id);
            if (existing == null)
            {
                return false;
            }

            _appointments.Remove(existing);
            return true;
        }
    }
}
