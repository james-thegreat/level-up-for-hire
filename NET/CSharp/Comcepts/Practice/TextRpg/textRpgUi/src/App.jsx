import { useState } from "react";

const API_BASE = "http://localhost:5283";

const heroImages = [
  { name: "Knight", url: "/characters/Knight.png" },
  { name: "Mage", url: "/characters/mage.png" },
  { name: "Rogue", url: "/characters/rogue.png" },
];

const enemyImages = [
  { type: "goblin", name: "Goblin", url: "/enemies/goblin.png" },
  { type: "slime", name: "Slime", url: "/enemies/slime.png" },
  { type: "dummy", name: "Training Dummy", url: "/enemies/dummy.png" }, // add this file or change url
];

const enemyImageByType = Object.fromEntries(enemyImages.map(e => [e.type, e.url]));

function normalizeImageUrl(url) {
  if (!url) return null;
  // If API returns "enemies/goblin.png", fix it to "/enemies/goblin.png"
  if (url.startsWith("http://") || url.startsWith("https://")) return url;
  if (url.startsWith("/")) return url;
  return `/${url}`;
}

export default function App() {
  const [name, setName] = useState("");
  const [imageUrl, setImageUrl] = useState(heroImages[0].url);
  const [character, setCharacter] = useState(null);

  const [enemyType, setEnemyType] = useState("goblin");
  const [combatId, setCombatId] = useState(null);
  const [enemy, setEnemy] = useState(null);

  const [lastAttack, setLastAttack] = useState(null);
  const [error, setError] = useState("");

  async function createCharacter(e) {
    e.preventDefault();
    setError("");
    setLastAttack(null);

    try {
      const res = await fetch(`${API_BASE}/api/Characters`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, imageUrl }),
      });

      const bodyText = await res.text();
      if (!res.ok) throw new Error(`HTTP ${res.status}: ${bodyText || "(empty)"}`);

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

      const data = await res.json();

      // ✅ Use API value if good, otherwise use local mapping by enemyType
      const apiUrl =
        data.imageUrl ?? data.enemyImageUrl ?? data.enemy?.imageUrl ?? null;

      const finalUrl =
        normalizeImageUrl(apiUrl) || enemyImageByType[enemyType] || "/enemies/unknown.png";

      setCombatId(data.combatId);

      setEnemy({
        name: data.enemyName ?? data.name ?? enemyType,
        imageUrl: finalUrl,
        maxHp: data.enemyMaxHp ?? data.maxHp,
        currentHp: data.enemyCurrentHp ?? data.currentHp,
      });
    } catch (err) {
      setError(err.message ?? String(err));
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

      if (!res.ok) throw new Error((await res.text()) || "Attack failed");

      const data = await res.json();
      setLastAttack(data);

      setEnemy(prev => (prev ? { ...prev, currentHp: data.enemyHpAfter } : prev));
      setCharacter(prev => (prev ? { ...prev, currentHp: data.characterHpAfter } : prev));

      if (data.enemyDefeated || data.characterDefeated) setCombatId(null);
    } catch (err) {
      setError(err.message ?? String(err));
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

      {/* CREATE CHARACTER */}
      <section style={{ marginTop: 24, padding: 16, border: "1px solid #ddd", borderRadius: 12 }}>
        <h2>Create Character</h2>

        <form onSubmit={createCharacter} style={{ display: "flex", gap: 8, flexDirection: "column" }}>
          <input
            value={name}
            onChange={(e) => setName(e.target.value)}
            placeholder="Character name"
            style={{ padding: 8 }}
          />

          <img
            src={imageUrl}
            alt="preview"
            style={{ width: 120, height: 120, objectFit: "cover", borderRadius: 12 }}
            onError={() => console.log("Hero image failed:", imageUrl)}
          />

          {/* ✅ ONLY HERO IMAGES HERE */}
          <select value={imageUrl} onChange={(e) => setImageUrl(e.target.value)} style={{ padding: 8 }}>
            {heroImages.map((img) => (
              <option key={img.url} value={img.url}>
                {img.name}
              </option>
            ))}
          </select>

          <button type="submit">Create</button>
        </form>

        {character && (
          <div style={{ marginTop: 16 }}>
            <h3>Current Character</h3>

            <img
              src={character.imageUrl}
              alt={character.name}
              style={{ width: 120, height: 120, objectFit: "cover", borderRadius: 12, marginBottom: 8 }}
              onError={() => console.log("Character image failed:", character.imageUrl)}
            />

            <p>
              <strong>HP:</strong> {character.currentHp}/{character.maxHp}
            </p>

            <pre style={{ background: "#1e1e1e", color: "#e6e6e6", padding: 12, borderRadius: 8 }}>
              {JSON.stringify(character, null, 2)}
            </pre>
          </div>
        )}
      </section>

      {/* COMBAT */}
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
              {enemyImages.map((e) => (
                <option key={e.type} value={e.type}>
                  {e.name}
                </option>
              ))}
            </select>
          </label>

          <button onClick={startCombat} disabled={!character}>
            Start Combat
          </button>

          <button onClick={attack} disabled={!character || !enemy || !combatId}>
            Attack
          </button>
        </div>

        {!character && <p>Create a character first.</p>}

        {enemy && (
          <div style={{ marginTop: 12 }}>
            <img
              src={enemy.imageUrl}
              alt={enemy.name}
              style={{ width: 120, height: 120, objectFit: "cover", borderRadius: 12, marginBottom: 8 }}
              onError={() => console.log("Enemy image failed:", enemy.imageUrl)}
            />

            <strong>Enemy:</strong> {enemy.name} — HP {enemy.currentHp}/{enemy.maxHp}
          </div>
        )}

        {lastAttack && (
          <div style={{ marginTop: 12 }}>
            <p>You took {lastAttack.damageToCharacter} damage</p>
          </div>
        )}
      </section>
    </div>
  );
}
