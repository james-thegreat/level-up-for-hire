using Xunit;

public class GameTests
{
    [Fact]
    public void PlayLevel_ReturnsTrue_WhenGuessIsCorrect()
    {
        var fakeInput = new FakeInput(new[] { 1 });
        var game = new Game(fakeInput);

        game.SetDifficultyForTest(difficultyMultiplier: 1, attempts: 1);

        // Act
        bool result = game.PlaySingleLevelForTest(1);

        // Assert
        Assert.True(result);
    }
}