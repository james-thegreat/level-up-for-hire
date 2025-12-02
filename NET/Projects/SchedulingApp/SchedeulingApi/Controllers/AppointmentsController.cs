using Microsoft.AspNetCore.Mvc;
using SchedulingApi.Models;
using SchedulingApi.Services;

namespace SchedulingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // => /api/appointments
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _service;

        // AppointmentService is injected by .NET (because we registered it in Program.cs)
        public AppointmentsController(AppointmentService service)
        {
            _service = service;
        }

        // GET /api/appointments
        [HttpGet]
        public ActionResult<List<Appointment>> GetAll()
        {
            var items = _service.GetAll();
            return Ok(items);
        }

        // POST /api/appointments
        [HttpPost]
        public ActionResult<Appointment> Create(Appointment request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Title is required.");
            }

            var created = _service.Add(new Appointment
            {
                Title = request.Title,
                StartTime = request.StartTime
            });

            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
