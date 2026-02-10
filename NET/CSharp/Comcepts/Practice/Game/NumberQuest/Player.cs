class Player
{
    public int Level { get; private set; }
    public int Score { get; private set; }

    public Player()
    {
        Level = 1;
        Score = 0;
    }

    public void LevelUp()
    {
        Level++;
    }

    public void AddScore (int points)
    {
        Score += points;
    }
}