namespace TextRpg.Domain;

public sealed class Enemy
{
    public string Name { get; }
    public int MaxHp { get; }
    public int CurrentHp { get; private set; }
    public int Attack { get; }
    public int Defense { get; }

    public bool IsDead => CurrentHp <= 0;

    public Enemy(string name, int maxHp, int attack, int defense)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Enemy name is requierd.", nameof(name));
        }
        if (maxHp <= 0) throw new ArgumentOutOfRangeException(nameof(maxHp));
        if (attack < 0) throw new ArgumentOutOfRangeException(nameof(attack));
        if (defense < 0) throw new ArgumentOutOfRangeException(nameof(defense));

        Name = name.Trim();
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Attack = attack;
        Defense = defense;
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        CurrentHp = Math.Max(0, CurrentHp - amount);
    }
}