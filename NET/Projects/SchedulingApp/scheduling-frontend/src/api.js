// src/api.js

// IMPORTANT: use http and the exact port from dotnet run
const API_BASE_URL = "http://localhost:5144/api";

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
