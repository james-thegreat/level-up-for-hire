import { useEffect, useState } from "react";
import { fetchAppointments, createAppointment } from "./api";

function App() {
  const [appointments, setAppointments] = useState([]);
  const [title, setTitle] = useState("");
  const [startTime, setStartTime] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  // Load appointments on first render
  useEffect(() => {
    const load = async () => {
      try {
        setLoading(true);
        const data = await fetchAppointments(); // <-- calls api.js, which calls backend
        setAppointments(data);
      } catch (err) {
        console.error(err);
        setError("Could not load appointments.");
      } finally {
        setLoading(false);
      }
    };

    load();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    if (!title || !startTime) {
      setError("Please fill in all fields.");
      return;
    }

    try {
      const newAppointment = {
        title,
        startTime: new Date(startTime).toISOString(),
      };

      const created = await createAppointment(newAppointment); // <-- create via backend

      setAppointments((prev) => [...prev, created]);
      setTitle("");
      setStartTime("");
    } catch (err) {
      console.error(err);
      setError("Failed to create appointment.");
    }
  };

  return (
    <div style={{ maxWidth: "600px", margin: "0 auto", padding: "1rem" }}>
      <h1>Scheduling App</h1>

      {error && (
        <div
          style={{
            backgroundColor: "#ffdddd",
            border: "1px solid #ffaaaa",
            padding: "0.5rem",
            marginBottom: "1rem",
          }}
        >
          {error}
        </div>
      )}

      <section style={{ marginBottom: "2rem" }}>
        <h2>Create Appointment</h2>
        <form onSubmit={handleSubmit}>
          <div style={{ marginBottom: "0.5rem" }}>
            <label>
              Title:{" "}
              <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="e.g. Dentist"
              />
            </label>
          </div>

          <div style={{ marginBottom: "0.5rem" }}>
            <label>
              Start Time:{" "}
              <input
                type="datetime-local"
                value={startTime}
                onChange={(e) => setStartTime(e.target.value)}
              />
            </label>
          </div>

          <button type="submit">Add Appointment</button>
        </form>
      </section>

      <section>
        <h2>Upcoming Appointments</h2>
        {loading ? (
          <p>Loading...</p>
        ) : appointments.length === 0 ? (
          <p>No appointments yet.</p>
        ) : (
          <ul>
            {appointments.map((a) => (
              <li key={a.id}>
                <strong>{a.title}</strong>{" "}
                <span>
                  –{" "}
                  {new Date(a.startTime).toLocaleString(undefined, {
                    dateStyle: "medium",
                    timeStyle: "short",
                  })}
                </span>
              </li>
            ))}
          </ul>
        )}
      </section>
    </div>
  );
}

export default App;
