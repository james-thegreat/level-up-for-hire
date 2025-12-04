import { useEffect, useState, useMemo } from "react";
import {
  fetchAppointments,
  createAppointment,
  updateAppointment,
  deleteAppointment,
} from "./api";

// Helper: get first and last day of the month
function getMonthInfo(year, monthIndex) {
  const firstDay = new Date(year, monthIndex, 1);
  const lastDay = new Date(year, monthIndex + 1, 0);
  return { firstDay, lastDay };
}

// Helper: format date as "YYYY-MM-DD" (local)
function toLocalDateKey(date) {
  const d = new Date(date);
  const year = d.getFullYear();
  const month = `${d.getMonth() + 1}`.padStart(2, "0");
  const day = `${d.getDate()}`.padStart(2, "0");
  return `${year}-${month}-${day}`;
}

// Helper: convert ISO date to datetime-local string
function toDatetimeLocalString(isoString) {
  const dt = new Date(isoString);
  const local = new Date(dt.getTime() - dt.getTimezoneOffset() * 60000);
  return local.toISOString().slice(0, 16); // "YYYY-MM-DDTHH:mm"
}

function App() {
  const [appointments, setAppointments] = useState([]);
  const [title, setTitle] = useState("");
  const [startTime, setStartTime] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const [editingId, setEditingId] = useState(null);

  // NEW: track which appointment is selected for the side panel
  const [selectedAppointment, setSelectedAppointment] = useState(null);

  // Calendar state: current month being viewed
  const [currentMonth, setCurrentMonth] = useState(() => {
    const now = new Date();
    return { year: now.getFullYear(), monthIndex: now.getMonth() }; // 0-based month
  });

  useEffect(() => {
    const load = async () => {
      try {
        setLoading(true);
        const data = await fetchAppointments();
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

  const resetForm = () => {
    setTitle("");
    setStartTime("");
    setEditingId(null);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    if (!title || !startTime) {
      setError("Please fill in all fields.");
      return;
    }

    const payload = {
      title,
      startTime: new Date(startTime).toISOString(),
    };

    try {
      if (editingId === null) {
        const created = await createAppointment(payload);
        setAppointments((prev) => [...prev, created]);
      } else {
        await updateAppointment(editingId, payload);
        setAppointments((prev) =>
          prev.map((a) =>
            a.id === editingId
              ? { ...a, title: payload.title, startTime: payload.startTime }
              : a
          )
        );
        // also update selected panel if it’s the same
        setSelectedAppointment((prev) =>
          prev && prev.id === editingId
            ? { ...prev, title: payload.title, startTime: payload.startTime }
            : prev
        );
      }
      resetForm();
    } catch (err) {
      console.error(err);
      setError(err.message || "Failed to save appointment.");
    }
  };

  const handleEditClick = (appointment) => {
    setError("");
    setEditingId(appointment.id);
    setTitle(appointment.title);
    setStartTime(toDatetimeLocalString(appointment.startTime));
    setSelectedAppointment(appointment); // keep side panel in sync
  };

  const handleCancelEdit = () => {
    resetForm();
  };

  const handleDeleteClick = async (id) => {
    setError("");
    try {
      await deleteAppointment(id);
      setAppointments((prev) => prev.filter((a) => a.id !== id));

      // If we were editing or viewing this one, clear state
      if (editingId === id) {
        resetForm();
      }
      setSelectedAppointment((prev) => (prev && prev.id === id ? null : prev));
    } catch (err) {
      console.error(err);
      setError(err.message || "Failed to delete appointment.");
    }
  };

  // NEW: when you click an event in the calendar
  const handleSelectAppointment = (appointment) => {
    setSelectedAppointment(appointment);
  };

  const handleClosePanel = () => {
    setSelectedAppointment(null);
  };

  const goToPrevMonth = () => {
    setCurrentMonth((prev) => {
      const m = prev.monthIndex - 1;
      if (m < 0) {
        return { year: prev.year - 1, monthIndex: 11 };
      }
      return { year: prev.year, monthIndex: m };
    });
  };

  const goToNextMonth = () => {
    setCurrentMonth((prev) => {
      const m = prev.monthIndex + 1;
      if (m > 11) {
        return { year: prev.year + 1, monthIndex: 0 };
      }
      return { year: prev.year, monthIndex: m };
    });
  };

  // Build calendar cells for the current month
  const { firstDay, lastDay } = useMemo(
    () => getMonthInfo(currentMonth.year, currentMonth.monthIndex),
    [currentMonth]
  );

  const calendarCells = useMemo(() => {
    const cells = [];
    const startWeekday = firstDay.getDay(); // 0 = Sunday
    const totalDays = lastDay.getDate();

    // Add empty cells before the 1st day
    for (let i = 0; i < startWeekday; i++) {
      cells.push({ type: "empty", key: `empty-${i}` });
    }

    // Add each day
    for (let day = 1; day <= totalDays; day++) {
      const dateObj = new Date(currentMonth.year, currentMonth.monthIndex, day);
      cells.push({
        type: "day",
        key: `day-${day}`,
        date: dateObj,
      });
    }

    return cells;
  }, [firstDay, lastDay, currentMonth]);

  // Group appointments by local date key for the current month
  const appointmentsByDate = useMemo(() => {
    const map = {};
    for (const appt of appointments) {
      const d = new Date(appt.startTime);
      if (
        d.getFullYear() === currentMonth.year &&
        d.getMonth() === currentMonth.monthIndex
      ) {
        const key = toLocalDateKey(d);
        if (!map[key]) map[key] = [];
        map[key].push(appt);
      }
    }
    // Sort appointments in each day by time
    for (const key of Object.keys(map)) {
      map[key].sort(
        (a, b) => new Date(a.startTime) - new Date(b.startTime)
      );
    }
    return map;
  }, [appointments, currentMonth]);

  const monthLabel = useMemo(() => {
    const d = new Date(currentMonth.year, currentMonth.monthIndex, 1);
    return d.toLocaleString(undefined, { month: "long", year: "numeric" });
  }, [currentMonth]);

  return (
    <div className="min-h-screen flex justify-center items-start py-10">
      <div className="w-full max-w-6xl px-4">
        {/* Header */}
        <header className="mb-6 flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
          <div>
            <h1 className="text-3xl font-bold text-slate-900">
              Scheduling App
            </h1>
            <p className="text-sm text-slate-600">
              Full-stack C# + React calendar with Tailwind and a side panel.
            </p>
          </div>
          <div className="flex items-center gap-2">
            <button
              onClick={goToPrevMonth}
              className="px-3 py-1 rounded-md border border-slate-300 bg-white text-sm hover:bg-slate-50"
            >
              ← Prev
            </button>
            <div className="font-semibold text-slate-800">{monthLabel}</div>
            <button
              onClick={goToNextMonth}
              className="px-3 py-1 rounded-md border border-slate-300 bg-white text-sm hover:bg-slate-50"
            >
              Next →
            </button>
          </div>
        </header>

        {/* Error message */}
        {error && (
          <div className="mb-4 rounded-md border border-red-300 bg-red-50 px-4 py-2 text-sm text-red-700">
            {error}
          </div>
        )}

        {/* Form card */}
        <section className="mb-6">
          <div className="rounded-xl bg-white shadow-md shadow-slate-200/60 p-4 sm:p-5">
            <h2 className="text-lg font-semibold text-slate-900 mb-3">
              {editingId === null ? "Create Appointment" : "Edit Appointment"}
            </h2>
            <form
              onSubmit={handleSubmit}
              className="flex flex-col gap-3 sm:flex-row sm:items-end sm:gap-4"
            >
              <div className="flex-1">
                <label className="block text-sm font-medium text-slate-700 mb-1">
                  Title
                </label>
                <input
                  type="text"
                  className="w-full rounded-md border border-slate-300 px-3 py-2 text-sm shadow-sm focus:border-sky-500 focus:outline-none focus:ring-1 focus:ring-sky-500"
                  value={title}
                  onChange={(e) => setTitle(e.target.value)}
                  placeholder="e.g. Dentist appointment"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-slate-700 mb-1">
                  Start Time
                </label>
                <input
                  type="datetime-local"
                  className="rounded-md border border-slate-300 px-3 py-2 text-sm shadow-sm focus:border-sky-500 focus:outline-none focus:ring-1 focus:ring-sky-500"
                  value={startTime}
                  onChange={(e) => setStartTime(e.target.value)}
                />
              </div>

              <div className="flex gap-2">
                <button
                  type="submit"
                  className="rounded-md bg-sky-600 px-4 py-2 text-sm font-medium text-white shadow-sm hover:bg-sky-700 disabled:opacity-50"
                  disabled={loading}
                >
                  {editingId === null ? "Add" : "Save"}
                </button>
                {editingId !== null && (
                  <button
                    type="button"
                    onClick={handleCancelEdit}
                    className="rounded-md border border-slate-300 bg-white px-3 py-2 text-sm text-slate-700 hover:bg-slate-50"
                  >
                    Cancel
                  </button>
                )}
              </div>
            </form>
          </div>
        </section>

        {/* Calendar + Side Panel layout */}
        <section className="grid gap-4 lg:grid-cols-[2fr,minmax(260px,320px)]">
          {/* Calendar */}
          <div className="rounded-xl bg-white shadow-md shadow-slate-200/60 p-4 sm:p-5">
            <div className="grid grid-cols-7 gap-px bg-slate-200 rounded-lg overflow-hidden text-xs font-medium text-slate-600 mb-1">
              {["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"].map((d) => (
                <div
                  key={d}
                  className="bg-slate-50 py-2 text-center uppercase tracking-wide"
                >
                  {d}
                </div>
              ))}
            </div>

            <div className="grid grid-cols-7 gap-px bg-slate-200 rounded-lg overflow-hidden">
              {calendarCells.map((cell) => {
                if (cell.type === "empty") {
                  return (
                    <div key={cell.key} className="bg-slate-50 h-20 sm:h-28" />
                  );
                }

                const dateKey = toLocalDateKey(cell.date);
                const dayAppointments = appointmentsByDate[dateKey] || [];

                const isToday = toLocalDateKey(new Date()) === dateKey;

                return (
                  <div
                    key={cell.key}
                    className="bg-white h-24 sm:h-32 p-1.5 flex flex-col border border-slate-100"
                  >
                    <div className="flex items-center justify-between mb-1">
                      <span
                        className={`text-xs font-semibold ${
                          isToday ? "text-sky-600" : "text-slate-700"
                        }`}
                      >
                        {cell.date.getDate()}
                      </span>
                      {isToday && (
                        <span className="inline-flex items-center rounded-full bg-sky-100 px-2 py-0.5 text-[10px] font-semibold text-sky-700">
                          Today
                        </span>
                      )}
                    </div>
                    <div className="flex-1 overflow-y-auto space-y-1">
                      {dayAppointments.length === 0 ? (
                        <span className="block text-[11px] text-slate-400">
                          No events
                        </span>
                      ) : (
                        dayAppointments.map((a) => (
                          <div
                            key={a.id}
                            className="group rounded-md border border-sky-100 bg-sky-50 px-1.5 py-1 text-[11px] text-slate-800 flex flex-col gap-0.5 cursor-pointer hover:bg-sky-100"
                            onClick={() => handleSelectAppointment(a)}
                          >
                            <div className="flex justify-between items-center gap-1">
                              <span className="font-semibold truncate">
                                {a.title}
                              </span>
                              <span className="text-[10px] text-slate-500 whitespace-nowrap">
                                {new Date(a.startTime).toLocaleTimeString(
                                  undefined,
                                  {
                                    hour: "2-digit",
                                    minute: "2-digit",
                                  }
                                )}
                              </span>
                            </div>
                            <div className="flex gap-1 opacity-0 group-hover:opacity-100 transition-opacity">
                              <button
                                type="button"
                                className="rounded-sm bg-white border border-sky-200 px-1 text-[10px] text-sky-700 hover:bg-sky-50"
                                onClick={(e) => {
                                  e.stopPropagation();
                                  handleEditClick(a);
                                }}
                              >
                                Edit
                              </button>
                              <button
                                type="button"
                                className="rounded-sm bg-white border border-rose-200 px-1 text-[10px] text-rose-700 hover:bg-rose-50"
                                onClick={(e) => {
                                  e.stopPropagation();
                                  handleDeleteClick(a.id);
                                }}
                              >
                                Delete
                              </button>
                            </div>
                          </div>
                        ))
                      )}
                    </div>
                  </div>
                );
              })}
            </div>
          </div>

          {/* Side Panel */}
          <div className="rounded-xl bg-white shadow-md shadow-slate-200/60 p-4 sm:p-5 min-h-[160px]">
            <h2 className="text-lg font-semibold text-slate-900 mb-3">
              Appointment Details
            </h2>

            {!selectedAppointment ? (
              <p className="text-sm text-slate-500">
                Select an appointment in the calendar to view its details here.
              </p>
            ) : (
              <div className="space-y-3">
                <div>
                  <h3 className="text-base font-semibold text-slate-900">
                    {selectedAppointment.title}
                  </h3>
                  <p className="text-sm text-slate-600">
                    {new Date(
                      selectedAppointment.startTime
                    ).toLocaleString(undefined, {
                      weekday: "long",
                      year: "numeric",
                      month: "long",
                      day: "numeric",
                      hour: "2-digit",
                      minute: "2-digit",
                    })}
                  </p>
                </div>

                <div className="border-t border-slate-200 pt-3 flex flex-wrap gap-2">
                  <button
                    type="button"
                    onClick={() => handleEditClick(selectedAppointment)}
                    className="rounded-md bg-sky-600 px-3 py-1.5 text-xs font-medium text-white shadow-sm hover:bg-sky-700"
                  >
                    Edit
                  </button>
                  <button
                    type="button"
                    onClick={() => handleDeleteClick(selectedAppointment.id)}
                    className="rounded-md bg-rose-600 px-3 py-1.5 text-xs font-medium text-white shadow-sm hover:bg-rose-700"
                  >
                    Delete
                  </button>
                  <button
                    type="button"
                    onClick={handleClosePanel}
                    className="rounded-md border border-slate-300 bg-white px-3 py-1.5 text-xs font-medium text-slate-700 hover:bg-slate-50"
                  >
                    Close
                  </button>
                </div>
              </div>
            )}
          </div>
        </section>
      </div>
    </div>
  );
}

export default App;
