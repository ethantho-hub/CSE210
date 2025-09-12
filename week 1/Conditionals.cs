using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the numeric grade (0-100): ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int grade) && grade >= 0 && grade <= 100)
        {
            char letterGrade;

            if (grade >= 90)
                letterGrade = 'A';
            else if (grade >= 80)
                letterGrade = 'B';
            else if (grade >= 70)
                letterGrade = 'C';
            else if (grade >= 60)
                letterGrade = 'D';
            else
                letterGrade = 'F';

            Console.WriteLine($"The letter grade is: {letterGrade}");
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number between 0 and 100.");
        }
    }
}
