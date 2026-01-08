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

        System.Console.WriteLine("Sum of an Array");
        for (int i = 0; i < numbers.Length; i ++)
        {
            sum = sum + numbers[i];
        }

        Console.WriteLine(sum);

        // -------------------------------------------

        // 2 Find the Largest Number -------------------
        int [] number = { 3, 7, 2, 9, 4 };

        int largest = 0;

        System.Console.WriteLine("Find the Largest Number");
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

        System.Console.WriteLine("Count Even & Odd Numbbers");
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

        System.Console.WriteLine("Grade Calculator");
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

        System.Console.WriteLine("\nReverse an Array");
        for (int f = nums.Length - 1; f >= 0; f--)
        {
            Console.WriteLine(nums[f]);
        }
        // -------------------------------------------

        // 6 Store History of Results ----------------
        int [] numz = { 5, 8, 2, 9 };
        int target = 8;

        System.Console.WriteLine("\nStore History of Results");
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


        System.Console.WriteLine("\nSimple Guessing Game");
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

        // #--------------Intermaedite --------------#

        // 1 Frequency Counter -----------------------
        int []fre = { 1, 2, 2, 3, 1, 4, 2 };
        int [] appears = { 0, 0, 0, 0 };

        for (int i = 0; i < fre.Length; i++)
        {
            if (fre[i] == 1)
            {
                appears[0] += 1;
            }
            else if (fre[i] == 2)
            {
                appears[1] += 1;
            }
            else if (fre[i] == 3)
            {
                appears [2] += 1;
            }
            else if (fre[i] == 4)
            {
                appears[3] += 1;
            }
        }

        System.Console.WriteLine("\nFrequency Counter");
        int count = 1;
        int countNum = 0;
        for (int i = 0; i < appears.Length; i++)
        {
            if (appears[i] == 1)
            {
            System.Console.WriteLine(count + " appears " + appears[countNum] + " time");
                
            }
            else
            {
            System.Console.WriteLine(count + " appears " + appears[countNum] + " times");
            }
            count++;
            countNum++;
        }
        // -------------------------------------------

        // 2 Second Largest Number -------------------
        int[] numSet = { 10, 5, 8, 20, 15 };
        int secondLargestNumber = 0;
        int largestNumber = 0;

        System.Console.WriteLine("\nSecond Largest Number");
        for (int i = 0; i < numSet.Length; i++)
        {
            if (numSet[i] < largestNumber)
            {
                largestNumber = numSet[i];
            }
            else if (numSet[i] > largestNumber)
            {
                secondLargestNumber = numSet[i];
            }
        }

        System.Console.WriteLine("Second largest " + secondLargestNumber);

        // -------------------------------------------
    }
}
