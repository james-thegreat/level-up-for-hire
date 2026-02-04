namespace TextRpg.Domain;

public sealed record CombatResult(
    int DamageToEnemy,
    int DamageToCharacter,
    int CharacterHpAfter,
    int EnemyHpAfter,
    bool EnemyDefeated,
    bool CharacterDefeated
);
