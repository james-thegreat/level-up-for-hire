using TextRpg.Domain;
using Xunit;

public class CharacterTests
{
    [Fact]
    public void NewCharacter_StartsWithFullHp()
    {
    //Given
    var c = new Character("James");
    Assert.Equal(c.MaxHp, c.CurrentHp);
    }

    [Fact]
    public void TakeDamage_CannotGoBelowZero()
    {
        var c = new Character("Hero");
        c.TakeDamage(999);
        Assert.Equal(0, c.CurrentHp);
    }

    [Fact]
    public void Heal_ConnotExceedMaxHp()
    {
        var c = new Character("Hero");
        c.TakeDamage(5);
        c.Heal(999);
        Assert.Equal(c.MaxHp, c.CurrentHp);
    }
}