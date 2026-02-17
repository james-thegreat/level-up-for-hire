import HealthBar from "./HealthBar";
import { heroImages } from "../data/images";

export default function CharacterPanel({
  name,
  setName,
  imageUrl,
  setImageUrl,
  character,
  onCreate,
}) {
  return (
    <section style={{ marginTop: 24, padding: 16, border: "1px solid #ddd", borderRadius: 12 }}>
      <h2>Create Character</h2>

      <form onSubmit={onCreate} style={{ display: "flex", gap: 8, flexDirection: "column" }}>
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
        />

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
          />

          <HealthBar label={`${character.name} HP`} current={character.currentHp} max={character.maxHp} />

          <pre style={{ background: "#1e1e1e", color: "#e6e6e6", padding: 12, borderRadius: 8 }}>
            {JSON.stringify(character, null, 2)}
          </pre>
        </div>
      )}
    </section>
  );
}
