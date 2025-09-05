using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        int magicNumber = random.Next(1, 101); // Random number between 1 and 100
        int guess = 0;
        int attempts = 0;

        Console.WriteLine("Welcome to the Guess My Number Game!");
        Console.WriteLine("I'm thinking of a number between 1 and 100.");
        Console.WriteLine("Try to guess it!\n");

        while (guess != magicNumber)
        {
            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out guess) || guess < 1 || guess > 100)
            {
                Console.WriteLine("Please enter a valid number between 1 and 100.");
                continue;
            }

            attempts++;

            if (guess < magicNumber)
            {
                Console.WriteLine("Too low! Try a higher number.");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Too high! Try a lower number.");
            }
            else
            {
                Console.WriteLine("Congratulations! You guessed the magic number" + {magicNumber} " in " {attempts} + " attempts.");
            }
        }
    }
}
