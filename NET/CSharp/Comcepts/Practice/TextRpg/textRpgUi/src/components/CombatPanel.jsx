import HealthBar from "./HealthBar";
import { enemyImages } from "../data/images";

export default function CombatPanel({
  character,
  enemyType,
  setEnemyType,
  enemy,
  combatId,
  lastAttack,
  onStartCombat,
  onAttack,
}) {
  return (
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

        <button onClick={onStartCombat} disabled={!character}>
          Start Combat
        </button>

        <button onClick={onAttack} disabled={!character || !enemy || !combatId}>
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
          />
          <HealthBar label={`${enemy.name} HP`} current={enemy.currentHp} max={enemy.maxHp} />
        </div>
      )}

      {lastAttack && (
        <div style={{ marginTop: 12 }}>
          <p>You took {lastAttack.damageToCharacter} damage</p>
        </div>
      )}
    </section>
  );
}
