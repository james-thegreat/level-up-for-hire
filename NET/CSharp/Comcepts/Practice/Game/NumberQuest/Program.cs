using System;

class Program
{
    // static Random random = new Random();
    // Game game = new Game();

    static void Main()
    {
        Console.Title = "Number Qest (OOP)";
        IInput input = new ConsoleInput();
        Game game = new Game(input);
        game.Start();
    }
}