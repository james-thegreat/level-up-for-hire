using System;
class Program
{
    static Random random = new Random();
    static void Main()
    {
        Console.Title = "Number Qest";
        PlayGame();
    }

    static void PlayGame()
    {
        int level = 1;
        int score = 0;
        bool playing = true;

        while (playing)
        {
            Console.Clear();
            System.Console.WriteLine($"=== LEVEL {level}");

            int maxNumber = level * 10;
            int attempts = 5;

            int secretNumber = random.Next(1, maxNumber + 1);

            bool won = PlayLevel(secretNumber, maxNumber, attempts);

            if (won)
            {
                score += level * 10;
                level++;

                System.Console.WriteLine("Level Complet!");
                System.Console.WriteLine($"Score: {score}");
                System.Console.WriteLine("Progress any key to continue...");
                Console.ReadKey();
            }
            else
            {
                System.Console.WriteLine("\n Game Over!");
                System.Console.WriteLine($"You reached  level {level}");
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
    
}