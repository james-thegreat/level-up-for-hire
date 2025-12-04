const API_BASE_URL = "http://localhost:5144/api"; // same as before

export async function fetchAppointments() {
  const res = await fetch(`${API_BASE_URL}/appointments`);
  if (!res.ok) {
    const text = await res.text();
    console.error("Fetch appointments failed:", res.status, text);
    throw new Error(
      text || `Failed to fetch appointments (status ${res.status})`
    );
  }
  return await res.json();
}

export async function createAppointment(appointment) {
  const res = await fetch(`${API_BASE_URL}/appointments`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(appointment),
  });

  const text = await res.text();

  if (!res.ok) {
    console.error("Create failed:", res.status, text);
    throw new Error(
      text || `Failed to create appointment (status ${res.status})`
    );
  }

  return JSON.parse(text);
}

// NEW: update an appointment
export async function updateAppointment(id, appointment) {
  const res = await fetch(`${API_BASE_URL}/appointments/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(appointment),
  });

  if (!res.ok) {
    const text = await res.text();
    console.error("Update failed:", res.status, text);
    throw new Error(
      text || `Failed to update appointment (status ${res.status})`
    );
  }

  // PUT returns 204 NoContent in our API, so nothing to parse
}

// NEW: delete an appointment
export async function deleteAppointment(id) {
  const res = await fetch(`${API_BASE_URL}/appointments/${id}`, {
    method: "DELETE",
  });

  if (!res.ok) {
    const text = await res.text();
    console.error("Delete failed:", res.status, text);
    throw new Error(
      text || `Failed to delete appointment (status ${res.status})`
    );
  }
}
