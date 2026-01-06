using System;


// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

class Program
{
    static void Main()
    {
        // 1 Sum of an Array ---------------------------
        int [] numbers = { 1, 2, 3, 4 };
        int sum = 0;

        for (int i = 0; i < numbers.Length; i ++)
        {
            sum = sum + numbers[i];
        }

        Console.WriteLine(sum);

        // -------------------------------------------

        // 2 Find the Largest Number -------------------
        int [] number = { 3, 7, 2, 9, 4 };

        int largest = 0;

        for (int j = 0; j < number.Length; j++){

            if (number[j] > largest)
            {
                largest = number[j];
            }
        }
        Console.WriteLine(largest);

        // -------------------------------------------
    
        // 3 Count Even & Odd Numbbers -----------------
        int [] num = {1, 2, 3, 4, 5, 6};
        int even = 0;
        int odd = 0;


        for (int k = 0; k < num.Length; k++)
        {
            if (k % 2 == 0)
            {
                even++;
            }
            else
            {
                odd++;
            }
        }

        Console.WriteLine($"Even: {even}");
        Console.WriteLine($"Odd: {odd}");
        // -------------------------------------------
        
        // 4 Grade Calculator ------------------------
        int score =  82;

        if (score < 100 && score >= 90 )
        {
            Console.WriteLine("Congrdulations you got a A");
        } 
        else if (score < 90 && score >=  80)
        {
            Console.WriteLine("Congrdulations you got a B");
        } 
        else if (score < 80 && score >= 70)
        {
            Console.WriteLine("Congrdulations you got a C");

        }
        else if (score < 70 && score >= 60)
        {
            Console.WriteLine("Congrdulations you got a D");

        }
        else
        {
            Console.WriteLine(" you got a F");
            
        }
        // -------------------------------------------

        // 5 reverse an Array ------------------------
        int[] nums = { 1, 2, 3, 4 };

        for (int f = nums.Length - 1; f >= 0; f--)
        {
            Console.WriteLine(nums[f]);
        }
        // -------------------------------------------

        // 6 Store History of Results ----------------
        int [] numz = { 5, 8, 2, 9 };
        int target = 8;

        for (int z = 0;  z < numz.Length; z++)
        {
            if (target == numz[z])
            {
                System.Console.WriteLine("Found");
            } else
            {
                System.Console.WriteLine("Not Found");
            }
           
        }
        // -------------------------------------------

        // 7 Simple Guessing Game --------------------
        Random rand = new Random();
        int guess = Convert.ToInt32(Console.ReadLine());
        int random = rand.Next(10); 
        System.Console.WriteLine(random);



        while (random != guess)
        {
            guess = Convert.ToInt32(Console.ReadLine());
             if (random == guess)
             {
                System.Console.WriteLine("Congadulations you guessed corect.");
             }
             else 
             {
                System.Console.WriteLine("Incorect try again.");
             }
        }
        // -------------------------------------------
    }
}
