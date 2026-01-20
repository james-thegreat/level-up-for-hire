using System;
public class Game
{
    private Random random;
    private Player player;
    private IInput input;

    private int difficultyMultiplier;
    private int attempts;
    

    public Game(IInput input)
    {
        this.input = input;
        player = new Player();
        random = new Random();
    }

    public void Start()
    {
        ChooseDifficulty();
        bool playing = true;

        while (playing)
        {

            Console.Clear();
            System.Console.WriteLine($"=== LEVEL {player.Level}");
        
            int maxNumber = player.Level * difficultyMultiplier;
            int secretNumber = random.Next(1, maxNumber + 1);

            bool won = PlayLevel(secretNumber, maxNumber);

            if (won)
            {
                player.AddScore(player.Level * 10);
                player.LevelUp();

                System.Console.WriteLine("\nLevel Complete!");
                System.Console.WriteLine($"Score: {player.Score}");
                System.Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                System.Console.WriteLine("\nGame Over!");
                System.Console.WriteLine($"Score: {player.Score}");
                System.Console.WriteLine("Press any key to continue...");
                playing = false;
            }
        }
    }

    private bool PlayLevel(int secretNumber, int maxNumber)
    {
        int remainingAttempts = attempts;

        while (remainingAttempts > 0)
        {
            System.Console.WriteLine($"\nGuess the number (1 - {maxNumber})");
            System.Console.WriteLine($"Attempts left: {remainingAttempts}");

            int guess = input.GetInt("Your guess: ");

            if (guess == secretNumber)
            {
                return true;
            }
            else if (guess < secretNumber)
            {
                System.Console.WriteLine("Too low!");
            }
            else
            {
                System.Console.WriteLine("Too hight!");
            }

            remainingAttempts--;
        }

        System.Console.WriteLine($"The correct number was {secretNumber}");
        return false;
    }

    private void ChooseDifficulty()
    {
        Console.Clear();
        System.Console.WriteLine("Choose Difficulty: ");
        System.Console.WriteLine("1. Easy");
        System.Console.WriteLine("2. Medium");
        System.Console.WriteLine("3. Hard");

        int choice = input.GetInt("Choice: ");

        switch (choice)
        {
            case 1:
                difficultyMultiplier = 5;
                attempts = 7;
                break;

            case 2:
                difficultyMultiplier = 10;
                attempts = 5;
                break;

            case 3:
                difficultyMultiplier = 20;
                attempts = 3;
                break;
        }
    }

    // For Unit Testing
    public bool PlaySingleLevelForTest(int secretNumber)
    {
        return PlayLevel(secretNumber, secretNumber);
    }

    public void SetDifficultyForTest(int difficultyMultiplier, int attempts)
    {
        this.difficultyMultiplier = difficultyMultiplier;
        this.attempts = attempts;
    }
    
    
}