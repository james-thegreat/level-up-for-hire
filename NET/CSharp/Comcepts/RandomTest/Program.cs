using System;

class Program
{

    static string AskNum()
    {
        Console.WriteLine("Enter Your guess: ");
        return Console.ReadLine();
    }


    //---------- V.2 -------------------------------------------
    static int AskForInt(string message)
    {
        while(true)
        {
            System.Console.WriteLine(message);
            string input = System.Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                return number;
            }
        }
    }
    //----------------------------------------------------------

    static void Main()
    {
        RandomService service = new RandomService();
        int number = service.GenRandNum();
        Console.WriteLine(number);

        Console.WriteLine("I'm thinking of a number between 1 and 10...");
        Console.WriteLine("You Have three chances to guess the number.");
        
    //---------- V.1 -------------------------------------------
        // string guess = AskNum();
        // int numguess = Convert.ToInt32(guess);
    //----------------------------------------------------------
    

        // Console.WriteLine($"your guess is {guess}");

        int i = 0;

        while (i < 3)
        {
            int guess = AskForInt("Enter your guess: ");
            if (number == guess)
            {
                // short cut for System.Console.WriteLine();
                // type "cw" then press tab (boom saved time).
                Console.WriteLine($"Correct the random number was {number}");
                break;
            }
            else
            {
                Console.WriteLine("Incorect the Random number is Try again.");
                //---------- V.2 -------------------------------------------
                // guess = AskForInt("Enter your guess: ");
                // ---------------------------------------------------------

                // --------- V.1---------------------------------------------
                // guess = AskNum();
                // numguess = Convert.ToInt32(guess);
                // ---------------------------------------------------------
            }
            i++;
        }

    }
}

// Debug log:
// - I tryed to use the input from the user but as im used to JavaScript and Python where 
//   i dont need to convert the string into a int i had to do this again.
// 
// - I ended up creating a function that terns the string input into a int to make things 
//   abit more easyer.
// 
// - i relised the second time i called the user input it made it that i was wasting wasting space 
//   as i was not reusing the inital user input.