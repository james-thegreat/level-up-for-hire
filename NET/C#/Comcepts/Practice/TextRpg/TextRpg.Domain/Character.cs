namespace TextRpg.Domain;

public sealed class Character
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public int Level { get; private set; } = 1;
    public int MaxHp { get; private set; } = 20;
    public int CurrentHp { get; private set; } = 20;
    public int Attack { get; private set; } = 5;
    public int Defense {get; private set; } = 2;

    public Character(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }
            Name = name.Trim();
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