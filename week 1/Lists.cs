using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        int input;

        Console.WriteLine("Enter numbers one at a time. Enter 0 to stop.\n");

        while (true)
        {
            Console.Write("Enter a number: ");
            string userInput = Console.ReadLine();

            if (!int.TryParse(userInput, out input))
            {
                Console.WriteLine("Please enter a valid integer.");
                continue;
            }

            if (input == 0)
            {
                break;
            }

            numbers.Add(input);
        }

        // Display the collected numbers
        Console.WriteLine("\nYou entered the following numbers:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}
