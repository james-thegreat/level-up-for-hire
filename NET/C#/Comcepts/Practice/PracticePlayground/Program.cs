using System;


// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

class Program
{
    static void Main()
    {
        // 1 Sum of an Array ---------------------------
        static void SumArray()
        {
            int [] numbers = { 1, 2, 3, 4 };
            int sum = 0;

            System.Console.WriteLine("Sum of an Array");
            for (int i = 0; i < numbers.Length; i ++)
            {
                sum = sum + numbers[i];
            }

            Console.WriteLine(sum);
        }
        // -------------------------------------------



        // 2 Find the Largest Number -------------------
        static void FindLargNum()
        {
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
        }
        // -------------------------------------------


        // 3 Count Even & Odd Numbbers -----------------
        static void EvanOdd()
        {
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
        }
        // -------------------------------------------

      
        // 4 Grade Calculator ------------------------
        static void GradeCalculator()
        {

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
        }
        // -------------------------------------------

 
        // 5 reverse an Array ------------------------
        static void ReverseArray()
        {
            int[] nums = { 1, 2, 3, 4 };

            System.Console.WriteLine("\nReverse an Array");
            for (int f = nums.Length - 1; f >= 0; f--)
            {
                Console.WriteLine(nums[f]);
            }
        }
        // -------------------------------------------

        // 6 Store History of Results ----------------
        static void StoreHistory()
        {
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
        }
        // -------------------------------------------

        // 7 Simple Guessing Game --------------------
        static void GuessingGame()
        {
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
        }
        // -------------------------------------------

        // #--------------Intermaedite --------------#


        // 8 Frequency Counter -----------------------
        static void FrequencyCounter()
        {
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
        }
        // -------------------------------------------

        // 9 Second Largest Number -------------------
        static void SecondLargestNumber()
        {
            int[] numSet = { 10, 5, 8, 20, 15 };
            int largestNumber = int.MinValue;
            int secondLargestNumber = int.MinValue;

            System.Console.WriteLine("\nSecond Largest Number");
            foreach (int i in numSet)
            {
                if (i > largestNumber)
                {
                    secondLargestNumber = largestNumber;
                    largestNumber = i;
                }
                else if (i > secondLargestNumber && i != largestNumber)
                {
                    secondLargestNumber = i;
                }
            }

            System.Console.WriteLine($"Second largest = {secondLargestNumber}");

        }
        // -------------------------------------------


        // 10 Validate a PIN --------------------------
    static void ValidatePin()
    {

        string pin;
        bool valid;

        do
        {
            System.Console.WriteLine("Enter 4-digit PIN: ");
            pin = Console.ReadLine();
            valid = true;

            if (pin.Length != 4)
            {
                valid = false;
            }

            foreach (char c in pin)
            {
                if (!char.IsDigit(c))
                {
                    valid = false;
                    break;
                }
            }

            if (!valid)
            {
                System.Console.WriteLine("Invalid PIN.");
            }


        } while (!valid);

        System.Console.WriteLine("PIN accepted.");

    }
        // ValidatePin();
        // -------------------------------------------

        // 11  Remove Duplicates from an Array
        static void RemoveDuplicates()
        {
            int[] numbers = { 1, 2, 2, 3, 1 };
            List<int> unique = new List<int>();

            foreach (int i in numbers)
            {
                if (!unique.Contains(i))
                {
                    unique.Add(i);
                }
            }

            foreach (int i in unique)
            {
                Console.Write(i + " ");
            }
        }
        // RemoveDuplicates();
        // -------------------------------------------
 
        // 12 Simple Banking System -------------------
        static void BankingSysyem()
        {
            decimal balance = 0;
            bool running = true;


            while (running)
            {
                    
                System.Console.WriteLine("Wellcome to the Bank Select an option: ");
                BankMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    System.Console.WriteLine("Deposit amount: ");
                        balance += decimal.Parse(Console.ReadLine());
                        break;
                    
                    case "2":
                        System.Console.WriteLine("Withdraw amount: ");
                        decimal amount = decimal.Parse(Console.ReadLine());

                        if (amount <= balance)
                        {
                            balance -= amount;
                        }
                        else
                        {
                            System.Console.WriteLine("Insufficent funds.");
                        }
                        break;
                    
                    case "3":
                        System.Console.WriteLine($"Balance: {balance}");
                        break;
                    
                    case "4":
                        running = false;
                        break;
                    
                    default:
                        System.Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        // 12 Menu Function
        static void BankMenu()
        {
            System.Console.WriteLine("1. Deposit");
            System.Console.WriteLine("2. Withdraw");
            System.Console.WriteLine("3. Vew Balance");
            System.Console.WriteLine("4. Exit");
        }
        // -------------------------------------------

        // 13 Word Counter ---------------------------
        static void WordCounter()
        {
            System.Console.WriteLine("Enter a Centance: ");
            string input = Console.ReadLine();

            string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            System.Console.WriteLine($"Word count: {words.Length}");
        }
        // -------------------------------------------

        // 14 Find the Missing Number ----------------
        static void FindMissingNumber()
        {
            int[] number = { 1, 2, 4, 5 };

            int n = number.Length + 1;
            int expectedSum = n * (n + 1) / 2;

            int actualSum = 0;
            foreach (int i in number)
            {
                actualSum += number;
            }

            System.Console.WriteLine($"Missing number = {expectedSum - actualSum}");

        }
        // -------------------------------------------

        // =============== Main Menu =================
        bool running = true;

        while (running)
        {
            Console.Clear();
            ShowMenu();

            System.Console.WriteLine("Chose an Option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SumArray();
                    break;

                case "2":
                    FindLargNum();
                    break;

                case "3":
                    EvanOdd();
                    break;

                case "4":
                    GradeCalculator();
                    break;

                case "5":
                    ReverseArray();
                    break;

                case "6":
                    StoreHistory();
                    break;

                case "7":
                    GuessingGame();
                    break;

                case "8":
                    FrequencyCounter();
                    break;

                case "9":
                    SecondLargestNumber();
                    break;

                case "10":
                    ValidatePin();
                    break;

                case "11":
                    RemoveDuplicates();
                    break;

                case "12":
                    BankingSysyem();
                    break;

                case "13":
                    WordCounter();
                    break;
                
                default:
                System.Console.WriteLine("Invalid option.");
                break;
            }
            System.Console.WriteLine("\nPress any key to continue..");
            Console.ReadKey();
        }

        // ------------------ MENU ---------------------
        static void ShowMenu()
        {
            System.Console.WriteLine("== C# Practice Playground ===");
            System.Console.WriteLine("1. SumArray");
            System.Console.WriteLine("2. FindLargNum");
            System.Console.WriteLine("3. EvanOdd");
            System.Console.WriteLine("4. GradeCalculator");
            System.Console.WriteLine("5. ReverseArray");
            System.Console.WriteLine("6. StoreHistory");
            System.Console.WriteLine("7. GuessingGame");
            System.Console.WriteLine("8. FrequencyCounter");
            System.Console.WriteLine("9. SecondLargestNumber");
            System.Console.WriteLine("10. ValidatePin");
            System.Console.WriteLine("11. RemoveDuplicates");
            System.Console.WriteLine("12. BankingSysyem");
            System.Console.WriteLine("13. WordCounter");
            System.Console.WriteLine("");
            System.Console.WriteLine("");
        }
        // =============================================

    }
}
