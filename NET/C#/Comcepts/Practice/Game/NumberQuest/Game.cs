namespace GameLogic;
public class Game
{
    static Random random = new Random();
    public static void PlayGame()
    {
        int level = 1;
        int score = 0;
        bool playing = true;

        int attempts;
        int difficultyMultiplier = ChooseDifficulty(out attempts);

        while (playing)
        {
            Console.Clear();
            System.Console.WriteLine($"=== LEVEL {level} ===");

            int maxNumber = level * difficultyMultiplier;
            int secretNumber = random.Next(1, maxNumber + 1);

            bool won = PlayLevel(secretNumber, maxNumber, attempts);

            if (won)
            {
                score += level * 10;
                level++;

                System.Console.WriteLine("Level Complet!");
                System.Console.WriteLine($"Score: {score}");
                System.Console.WriteLine($"Difficulty Multiplier: {difficultyMultiplier}");
                System.Console.WriteLine("Progress any key to continue...");
                Console.ReadKey();
            }
            else
            {
                System.Console.WriteLine("\n Game Over!");
                System.Console.WriteLine($"You reached  level {level}");
                System.Console.WriteLine($"Difficulty Multiplier: {difficultyMultiplier}");
                System.Console.WriteLine($"Final score: {score}");
                playing = false;
            }
        }
    }

    static bool PlayLevel(int secretNumber, int maxNumber, int attempts)
    {
        while (attempts > 0)
        {
            System.Console.WriteLine($"\nGuess the number(1 - {maxNumber})");
            System.Console.WriteLine($"Attempts left: {attempts}");
            int guess = GetValidInt("Your guess: ");

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
                System.Console.WriteLine("Too high!");
            }

            attempts--;
        }

        System.Console.WriteLine($"\nThe correct number was {secretNumber}");
        return false;
    }

    static int GetValidInt(string message)
    {
        int value;
        bool success;

        do
        {
            Console.Write(message);
            success = int.TryParse(Console.ReadLine(), out value);
            if (!success)
            {
                System.Console.WriteLine("Invalid number, Try again.");
            }

        } while (!success);

        return value;
    }

    static int ChooseDifficulty(out int attempts)
    {
        Console.Clear();
        Console.WriteLine("Choose Difficulty: ");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3, Hard");

        int choice = GetValidInt("Your choice: ");

        switch (choice)
        {
            case 1:
                attempts = 7;
                return 5;
                
            case 2:
                attempts = 5;
                return 10;

            case 3:
                attempts = 3;
                return 20;

            default:
            System.Console.WriteLine("Invalid choice. Defaulting to Medium.");
            attempts = 5;
            return 10;
        }
    }
}