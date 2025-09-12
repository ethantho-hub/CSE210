using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JournalApp
{
    // A single journal entry
    public class Entry
    {
        public DateTime Date { get; set; }
        public string Prompt { get; set; } = "";
        public string Text { get; set; } = "";

        // Display method encapsulates how an Entry is shown
        public void Display()
        {
            Console.WriteLine("----- Entry -----");
            Console.WriteLine($"Date: {Date:yyyy-MM-dd HH:mm}");
            if (!string.IsNullOrWhiteSpace(Prompt))
                Console.WriteLine($"Prompt: {Prompt}");
            Console.WriteLine("Text:");
            Console.WriteLine(Text);
            Console.WriteLine("-----------------\n");
        }
    }

    // Stores and manages multiple entries
    public class Journal
    {
        private readonly List<Entry> _entries = new();

        public void AddEntry(Entry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            _entries.Add(entry);
        }

        public IReadOnlyList<Entry> Entries => _entries.AsReadOnly();

        public void DisplayAll()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("No entries yet.\n");
                return;
            }

            foreach (var e in _entries)
                e.Display();
        }

        public void SaveToFile(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_entries, options);
            File.WriteAllText(path, json);
            Console.WriteLine($"Journal saved to {path}\n");
        }

        public void LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"No file found at {path}. Starting with an empty journal.\n");
                return;
            }

            string json = File.ReadAllText(path);
            try
            {
                var items = JsonSerializer.Deserialize<List<Entry>>(json);
                _entries.Clear();
                if (items != null) _entries.AddRange(items);
                Console.WriteLine($"Loaded {_entries.Count} entries from {path}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load journal: {ex.Message}\n");
            }
        }
    }

    // Simple prompt generator with a few example prompts
    public class PromptGenerator
    {
        private readonly List<string> _prompts = new()
        {
            "What made you smile today?",
            "Describe a challenge you overcame recently.",
            "Write about a lesson you learned this week.",
            "What are you grateful for right now?",
            "How do you want to grow in the next month?"
        };

        private readonly Random _rng = new();

        public string GetRandomPrompt()
        {
            if (_prompts.Count == 0) return "";
            int idx = _rng.Next(_prompts.Count);
            return _prompts[idx];
        }

        public void AddPrompt(string prompt)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
                _prompts.Add(prompt.Trim());
        }

        public IReadOnlyList<string> Prompts => _prompts.AsReadOnly();
    }

    class Program
    {
        private const string DefaultFile = "journal.json";

        static void Main()
        {
            var journal = new Journal();
            var prompts = new PromptGenerator();

            // Try to load existing journal (if present)
            journal.LoadFromFile(DefaultFile);

            ShowMenu();

            bool quit = false;
            while (!quit)
            {
                Console.Write("Choose an option (1-7): ");
                var input = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        AddEntryFlow(journal, prompts);
                        break;
                    case "2":
                        journal.DisplayAll();
                        break;
                    case "3":
                        journal.SaveToFile(DefaultFile);
                        break;
                    case "4":
                        journal.LoadFromFile(DefaultFile);
                        break;
                    case "5":
                        ShowPrompts(prompts);
                        break;
                    case "6":
                        AddCustomPrompt(prompts);
                        break;
                    case "7":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.\n");
                        break;
                }
            }

            Console.WriteLine("Goodbye — your journal session ended.");
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== Journal App ===");
            Console.WriteLine("1) Add a new entry (uses a random prompt)");
            Console.WriteLine("2) Display all entries");
            Console.WriteLine("3) Save journal to file");
            Console.WriteLine("4) Load journal from file");
            Console.WriteLine("5) Show available prompts");
            Console.WriteLine("6) Add a custom prompt");
            Console.WriteLine("7) Quit");
            Console.WriteLine();
        }

        static void AddEntryFlow(Journal journal, PromptGenerator prompts)
        {
            Console.WriteLine("Add a new entry");
            string prompt = prompts.GetRandomPrompt();
            Console.WriteLine($"Prompt: {prompt}");
            Console.WriteLine("Type your entry below. Enter an empty line to finish.");

            // Read multi-line input until an empty line is entered
            var lines = new List<string>();
            while (true)
            {
                string? line = Console.ReadLine();
                if (line == null || string.IsNullOrEmpty(line)) break;
                lines.Add(line);
            }

            string text = string.Join(Environment.NewLine, lines).Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("No text entered; entry canceled.\n");
                return;
            }

            var entry = new Entry
            {
                Date = DateTime.Now,
                Prompt = prompt,
                Text = text
            };

            journal.AddEntry(entry);
            Console.WriteLine("Entry added.\n");
        }

        static void ShowPrompts(PromptGenerator prompts)
        {
            Console.WriteLine("Available prompts:");
            int i = 1;
            foreach (var p in prompts.Prompts)
                Console.WriteLine($"{i++}. {p}");
            Console.WriteLine();
        }

        static void AddCustomPrompt(PromptGenerator prompts)
        {
            Console.Write("Enter the text for the new prompt: ");
            string? newPrompt = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newPrompt))
            {
                Console.WriteLine("Prompt cannot be empty.\n");
                return;
            }
            prompts.AddPrompt(newPrompt);
            Console.WriteLine("Prompt added.\n");
        }
    }
}
