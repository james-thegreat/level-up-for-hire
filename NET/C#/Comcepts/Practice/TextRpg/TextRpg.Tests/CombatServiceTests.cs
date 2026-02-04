using TextRpg.Domain;
using Xunit;

public class CombatServiceTests
{
    [Fact]
    public void Attack_DamageIsAtLeastOne()
    {
        var hero = new Character("Hero"); // Attack=5, Defense=2
        var tank = new Enemy("Tank", maxHp: 10, attack: 1, defense: 999);

        var service = new CombatService();
        var result = service.Attack(hero, tank);

        Assert.Equal(1, result.DamageToEnemy);
        Assert.Equal(9, result.EnemyHpAfter);
    }

    [Fact]
    public void Attack_WhenEnemySurvives_EnemyHitsBack()
    {
        var hero = new Character("Hero"); // HP=20
        var goblin = new Enemy("Goblin", maxHp: 10, attack: 4, defense: 0);

        var service = new CombatService();
        var result = service.Attack(hero, goblin);

        // Hero deals max(1,5-0)=5 so goblin to 5 HP
        Assert.Equal(5, result.DamageToEnemy);
        Assert.Equal(5, result.EnemyHpAfter);

        // Goblin hits back for max(1,4-2)=2 so hero gose to 18 HP
        Assert.Equal(2, result.DamageToCharacter);
        Assert.Equal(18, result.CharacterHpAfter);
    }

    [Fact]
    public void Attack_WhenEnemyDies_EnemyDoseNotHitBack()
    {
        var hero = new Character("Hero");
        var weak = new Enemy("Weakling", maxHp: 1, attack: 999, defense: 0);

        var service = new CombatService();
        var result = service.Attack(hero, weak);

        Assert.True(result.EnemyDefeated);
        Assert.Equal(0, result.EnemyHpAfter);

        Assert.Equal(0, result.DamageToCharacter);
        Assert.Equal(hero.MaxHp, result.CharacterHpAfter);
    }
}