public sealed class Enemy
{
    public string Name { get; }
    public string ImageUrl { get; }   // ✅ NEW
    public int MaxHp { get; }
    public int CurrentHp { get; private set; }
    public int Attack { get; }
    public int Defense { get; }

    public bool IsDead => CurrentHp <= 0;

    public Enemy(string name, string imageUrl, int maxHp, int attack, int defense)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Enemy name is required.", nameof(name));

        Name = name.Trim();
        ImageUrl = imageUrl;

        MaxHp = maxHp;
        CurrentHp = maxHp;
        Attack = attack;
        Defense = defense;
    }

    public void TakeDamage(int amount)
    {
        CurrentHp = Math.Max(0, CurrentHp - amount);
    }
}
