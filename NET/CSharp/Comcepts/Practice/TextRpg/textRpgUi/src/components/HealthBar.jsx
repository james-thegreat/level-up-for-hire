export default function HealthBar({ current, max, label = "HP" }) {
  const safeMax = Math.max(1, Number(max ?? 1));
  const safeCurrent = Math.max(0, Math.min(Number(current ?? 0), safeMax));
  const pct = Math.round((safeCurrent / safeMax) * 100);

  return (
    <div style={{ width: 260 }}>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          marginBottom: 6,
        }}
      >
        <strong>{label}</strong>
        <span>
          {safeCurrent}/{safeMax} ({pct}%)
        </span>
      </div>

      <div
        style={{
          height: 16,
          background: "#eee",
          borderRadius: 999,
          overflow: "hidden",
          border: "1px solid #ddd",
        }}
      >
        <div
          style={{
            height: "100%",
            width: `${pct}%`,
            background: pct > 50 ? "#22c55e" : pct > 20 ? "#f59e0b" : "#ef4444",
            transition: "width 200ms ease",
          }}
        />
      </div>
    </div>
  );
}
