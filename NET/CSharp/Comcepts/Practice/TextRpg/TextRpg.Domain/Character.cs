namespace TextRpg.Domain;

public sealed class Character
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public string ImageUrl { get; private set; }  // ✅

    public int Level { get; private set; } = 1;
    public int MaxHp { get; private set; } = 20;
    public int CurrentHp { get; private set; } = 20;
    public int Attack { get; private set; } = 5;
    public int Defense { get; private set; } = 2;

    public bool IsDead => CurrentHp <= 0;

    // ✅ constructor MUST include imageUrl parameter
    public Character(string name, string imageUrl = "/characters/Knight.png")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        Name = name.Trim();

        ImageUrl = string.IsNullOrWhiteSpace(imageUrl)
            ? "/characters/Knight.png"
            : imageUrl.Trim();
    }


    public void TakeDamage(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        CurrentHp = Math.Max(0, CurrentHp - amount);
    }

    public void Heal(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        CurrentHp = Math.Min(MaxHp, CurrentHp + amount);
    }
}
