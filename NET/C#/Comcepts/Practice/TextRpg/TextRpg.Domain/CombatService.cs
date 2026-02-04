namespace TextRpg.Domain;

public sealed class CombatService
{
    // Verry simple deterministic formula (great for testing): 
    // damage = max(1, attacker.Attack - defender.Defense)
    public CombatResult Attack(Character character, Enemy enemy)
    {
        if (character is null) throw new ArgumentNullException(nameof(character));
        if (enemy is null) throw new ArgumentNullException(nameof(enemy));
        if (character.IsDead) throw new InvalidOperationException("Character is defeated and can't act.");
        if (enemy.IsDead) throw new InvalidOperationException("Enemy is already defeated.");

        int damageToEnemy = CalculateDamage(character.Attack, enemy.Defense);
        enemy.TakeDamage(damageToEnemy);

        int damageToCharacter = 0;
        if (!enemy.IsDead)
        {
            damageToCharacter = CalculateDamage(enemy.Attack, character.Defense);
            character.TakeDamage(damageToCharacter);
        }

        return new CombatResult(
            DamageToEnemy: damageToEnemy,
            DamageToCharacter: damageToCharacter,
            CharacterHpAfter: character.CurrentHp,
            EnemyHpAfter: enemy.CurrentHp,
            EnemyDefeated: enemy.IsDead,
            CharacterDefeated: character.IsDead
        );

    }

    private static int CalculateDamage(int attack, int defense)
        => Math.Max(1, attack - defense);
}