using System;

class Program
{
    static void Main()
    {
        DisplayWelcome();

        string name = PromptUserName();
        int favoriteNumber = PromptUserNumber();
        int squaredNumber = SquareNumber(favoriteNumber);

        DisplayResult(name, squaredNumber);
    }

    // Function 1: Display welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Function 2: Prompt for user's name
    static string PromptUserName()
    {
        Console.Write("What is your name? ");
        return Console.ReadLine();
    }

    // Function 3: Prompt for user's favorite number
    static int PromptUserNumber()
    {
        int number;
        while (true)
        {
            Console.Write("What is your favorite number? ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out number))
            {
                return number;
            }
            else
            {
                Console.WriteLine("Please enter a valid integer.");
            }
        }
    }

    // Function 4: Square the number
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Function 5: Display the result
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"\n{name}, the square of your number is {squaredNumber}.");
    }
}
