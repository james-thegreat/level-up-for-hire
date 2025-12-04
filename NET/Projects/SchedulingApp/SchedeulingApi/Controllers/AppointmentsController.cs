using Microsoft.AspNetCore.Mvc;
using SchedulingApi.Models;
using SchedulingApi.Services;

namespace SchedulingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // /api/appointments
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _service;

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

        // GET /api/appointments/{id}
        [HttpGet("{id:int}")]
        public ActionResult<Appointment> GetById(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
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

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT /api/appointments/{id}
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Appointment request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Title is required.");
            }

            var success = _service.Update(id, new Appointment
            {
                Title = request.Title,
                StartTime = request.StartTime
            });

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // 204
        }

        // DELETE /api/appointments/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // 204
        }
    }
}
