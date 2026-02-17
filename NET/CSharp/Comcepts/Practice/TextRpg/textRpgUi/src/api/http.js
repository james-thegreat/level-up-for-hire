const API_BASE = import.meta.env.VITE_API_BASE;

async function parseBody(res) {
  const contentType = res.headers.get("content-type") || "";
  if (contentType.includes("application/json")) return await res.json();
  const text = await res.text();
  return text ? text : null;
}

export async function apiFetch(path, options = {}) {
  const res = await fetch(`${API_BASE}${path}`, {
    headers: { "Content-Type": "application/json", ...(options.headers || {}) },
    ...options,
  });

  const body = await parseBody(res);

  if (!res.ok) {
    const message =
      typeof body === "string"
        ? body
        : body?.message || `HTTP ${res.status} ${res.statusText}`;
    throw new Error(message);
  }

  return body;
}
