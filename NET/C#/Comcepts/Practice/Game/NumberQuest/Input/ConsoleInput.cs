using System;

public class ConsoleInput : IInput
{
    public int GetInt(string message)
    {
        int value;
        bool success;

        do
        {
            System.Console.WriteLine(message);
            success = int.TryParse(Console.ReadLine(), out value);

            if (!success)
            {
                System.Console.WriteLine("Invalid number.");
            } 

        } while (!success);

        return value;
    }
}