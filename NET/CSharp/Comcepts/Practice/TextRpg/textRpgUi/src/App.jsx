import { useState } from "react";
import { apiFetch } from "./api/http";
import CharacterPanel from "./components/CharacterPanel";
import CombatPanel from "./components/CombatPanel";
import { heroImages, enemyImageByType, normalizeImageUrl } from "./data/images";

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
      const data = await apiFetch("/api/Characters", {
        method: "POST",
        body: JSON.stringify({ name, imageUrl }),
      });

      setCharacter(data);
      setName("");
    } catch (err) {
      setError(err.message ?? String(err));
    }
  }

  async function startCombat() {
    setError("");
    setLastAttack(null);

    try {
      const data = await apiFetch("/api/combats/start", {
        method: "POST",
        body: JSON.stringify({ enemyType }),
      });

      const apiUrl = data.imageUrl ?? data.enemyImageUrl ?? data.enemy?.imageUrl ?? null;
      const finalUrl = normalizeImageUrl(apiUrl) || enemyImageByType[enemyType] || "/images/enemies/dummy.png";

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
      const data = await apiFetch(`/api/combats/${combatId}/attack`, {
        method: "POST",
        body: JSON.stringify({ characterId: character.id }),
      });

      setLastAttack(data);

      setEnemy((prev) => (prev ? { ...prev, currentHp: data.enemyHpAfter } : prev));
      setCharacter((prev) => (prev ? { ...prev, currentHp: data.characterHpAfter } : prev));

      if (data.enemyDefeated || data.characterDefeated) setCombatId(null);
    } catch (err) {
      setError(err.message ?? String(err));
    }
  }

  return (
    <div style={{ fontFamily: "system-ui", padding: 24, maxWidth: 800 }}>
      <h1>TextRPG UI (Vite + React)</h1>

      {error && (
        <div style={{ background: "#fee", padding: 12, borderRadius: 8 }}>
          <strong>Error:</strong> {error}
        </div>
      )}

      <CharacterPanel
        name={name}
        setName={setName}
        imageUrl={imageUrl}
        setImageUrl={setImageUrl}
        character={character}
        onCreate={createCharacter}
      />

      <CombatPanel
        character={character}
        enemyType={enemyType}
        setEnemyType={setEnemyType}
        enemy={enemy}
        combatId={combatId}
        lastAttack={lastAttack}
        onStartCombat={startCombat}
        onAttack={attack}
      />
    </div>
  );
}
