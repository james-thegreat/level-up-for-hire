export const heroImages = [
  { name: "Knight", url: "/images/characters/knight.png" },
  { name: "Mage", url: "/images/characters/mage.png" },
  { name: "Rogue", url: "/images/characters/rogue.png" },
];

export const enemyImages = [
  { type: "goblin", name: "Goblin", url: "/images/enemies/goblin.png" },
  { type: "slime", name: "Slime", url: "/images/enemies/slime.png" },
  { type: "dummy", name: "Training Dummy", url: "/images/enemies/dummy.png" },
];

export const enemyImageByType = Object.fromEntries(
  enemyImages.map((e) => [e.type, e.url]),
);

export function normalizeImageUrl(url) {
  if (!url) return null;
  if (url.startsWith("http://") || url.startsWith("https://")) return url;
  if (url.startsWith("/")) return url;
  return `/${url}`;
}
