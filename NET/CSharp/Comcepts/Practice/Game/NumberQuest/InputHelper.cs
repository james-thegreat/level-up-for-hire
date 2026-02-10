using System;

class InputHelper
{
    public static int GetValidInt(string message)
    {
        int value;
        bool success;

        do
        {
            System.Console.WriteLine(message);
            success = int.TryParse(Console.ReadLine(), out value);

            if (!success)
            {
                System.Console.WriteLine("Invalid number, try again.");
            }
        } while (!success);

        return value;
    }


}