using System;
using System.Collections.Generic;
using System.Linq;

class Scripture
{
    public string Reference { get; }
    private List<Word> Words { get; }

    public Scripture(string reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ')
                    .Select(w => new Word(w))
                    .ToList();
    }

    public void Display()
    {
        Console.WriteLine($"{Reference}");
        Console.WriteLine();
        foreach (var w in Words)
        {
            Console.Write(w.GetDisplayText() + " ");
        }
        Console.WriteLine();
    }

    public bool HideRandomWords(int count)
    {
        // Pick only words that are not already hidden
        var visible = Words.Where(w => !w.Hidden).ToList();
        if (visible.Count == 0) return false;

        var rand = new Random();
        for (int i = 0; i < count && visible.Count > 0; i++)
        {
            int index = rand.Next(visible.Count);
            visible[index].Hide();
            visible.RemoveAt(index);
        }
        return true;
    }

    public bool AllHidden() => Words.All(w => w.Hidden);
}

class Word
{
    private readonly string _text;
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        _text = text;
        Hidden = false;
    }

    public void Hide() => Hidden = true;

    public string GetDisplayText()
    {
        return Hidden
            ? new string('_', _text.Length)
            : _text;
    }
}

class Program
{
    static void Main()
    {
        // Example scripture – change to any reference/text you like
        var scripture = new Scripture(
            "Proverbs 3:5-6",
            "Trust in the Lord with all thine heart and lean not unto thine own understanding. " +
            "In all thy ways acknowledge him and he shall direct thy paths."
        );

        while (true)
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine();
            Console.WriteLine("Press Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "quit")
                break;

            // Hide a few random words (here 3 each time)
            bool stillVisible = scripture.HideRandomWords(3);

            if (!stillVisible || scripture.AllHidden())
            {
                Console.Clear();
                scripture.Display();
                Console.WriteLine("\nAll words are hidden. Program ending.");
                break;
            }
        }
    }
}
