import { useState } from "react";

const API_BASE = "http://localhost:5283";

export default function App() {
  // Character state
  const [name, setName] = useState("");
  const [character, setCharacter] = useState(null);

  // Combat state
  const [enemyType, setEnemyType] = useState("goblin");
  const [combatId, setCombatId] = useState(null);
  const [enemy, setEnemy] = useState(null);

  // Combat result
  const [lastAttack, setLastAttack] = useState(null);
  const [error, setError] = useState("");

async function createCharacter(e) {
  e.preventDefault();
  setError("");
  setLastAttack(null);

  try {
    console.log("POST", `${API_BASE}/api/Characters`, { name });

    const res = await fetch(`${API_BASE}/api/Characters`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ name }),
    });

    const bodyText = await res.text(); // read even on error
    console.log("STATUS", res.status, "BODY", bodyText);

    if (!res.ok) {
      throw new Error(`HTTP ${res.status}: ${bodyText || "(empty response)"}`);
    }

    const data = JSON.parse(bodyText);
    setCharacter(data);
    setName("");
  } catch (err) {
    setError(String(err));
  }
}


  async function startCombat() {
    setError("");
    setLastAttack(null);

    try {
      const res = await fetch(`${API_BASE}/api/combats/start`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ enemyType }),
      });

      if (!res.ok) {
        const text = await res.text();
        throw new Error(text || "Failed to start combat");
      }

      const data = await res.json();
      setCombatId(data.combatId);
      setEnemy({
        name: data.enemyName,
        maxHp: data.enemyMaxHp,
        currentHp: data.enemyCurrentHp,
      });
    } catch (err) {
      setError(err.message);
    }
  }

  async function attack() {
    if (!character || !combatId) return;

    setError("");

    try {
      const res = await fetch(`${API_BASE}/api/combats/${combatId}/attack`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ characterId: character.id }),
      });

      if (!res.ok) {
        const text = await res.text();
        throw new Error(text || "Attack failed");
      }

      const data = await res.json();
      setLastAttack(data);

      // Update enemy HP shown in UI
      setEnemy((prev) =>
        prev ? { ...prev, currentHp: data.enemyHpAfter } : prev
      );

      // Fetch updated character from API so UI shows new HP
      const chRes = await fetch(`${API_BASE}/api/Characters/${character.id}`);
      const updatedCharacter = await chRes.json();
      setCharacter(updatedCharacter);

      // If fight ended, clear combat
      if (data.enemyDefeated || data.characterDefeated) {
        setCombatId(null);
      }
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div style={{ fontFamily: "system-ui", padding: 24, maxWidth: 800 }}>
      <h1>TextRPG UI (React)</h1>

      {error && (
        <div style={{ background: "#fee", padding: 12, borderRadius: 8 }}>
          <strong>Error:</strong> {error}
        </div>
      )}

      <section style={{ marginTop: 24, padding: 16, border: "1px solid #ddd", borderRadius: 12 }}>
        <h2>Create Character</h2>
        <form onSubmit={createCharacter} style={{ display: "flex", gap: 8 }}>
          <input
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Character name"
            style={{ flex: 1, padding: 8 }}
          />
          <button type="submit">Create</button>
        </form>

        {character && (
          <div style={{ marginTop: 12 }}>
            <h3>Current Character</h3>
            <p>
              <strong>HP:</strong> {character.currentHp}/{character.maxHp}
            </p>

            <pre style={{ background: "#f6f6f6", color: "#111", padding: 12, borderRadius: 8 }}>
              {JSON.stringify(character, null, 2)}
            </pre>
          </div>
        )}
      </section>

      <section style={{ marginTop: 24, padding: 16, border: "1px solid #ddd", borderRadius: 12 }}>
        <h2>Combat</h2>

        <div style={{ display: "flex", gap: 8, alignItems: "center" }}>
          <label>
            Enemy:
            <select
              value={enemyType}
              onChange={(e) => setEnemyType(e.target.value)}
              style={{ marginLeft: 8, padding: 6 }}
            >
              <option value="goblin">Goblin</option>
              <option value="slime">Slime</option>
              <option value="dummy">Training Dummy</option>
            </select>
          </label>

          <button onClick={startCombat} disabled={!character}>
            Start Combat
          </button>

          <button onClick={attack} disabled={!character || !enemy || !combatId}>
            Attack
          </button>
        </div>

        {!character && <p style={{ marginTop: 12 }}>Create a character first.</p>}

        {enemy && (
          <div style={{ marginTop: 12 }}>
            <strong>Enemy:</strong> {enemy.name} — HP {enemy.currentHp}/{enemy.maxHp}
            {combatId && (
              <div style={{ fontSize: 12, opacity: 0.7 }}>CombatId: {combatId}</div>
            )}
          </div>
        )}

        {lastAttack && (
          <div style={{ marginTop: 12 }}>
            <h3>Last Attack Result</h3>
            <pre style={{ background: "#f6f6f6", color: "#111", padding: 12, borderRadius: 8 }}>
              {JSON.stringify(lastAttack, null, 2)}
            </pre>
          </div>
        )}
      </section>
    </div>
  );
}
