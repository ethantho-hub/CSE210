using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace MindfulnessApp
{
    // Abstract base class for shared behavior
    abstract class Activity
    {
        private string _name;
        private string _description;
        private int _durationSeconds;

        protected Random _rnd = new Random();

        public Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        // Ask user for duration and store it
        public void Start()
        {
            Console.Clear();
            ShowStartingMessage();
            _durationSeconds = PromptForDuration();
            PrepareToBegin();
        }

        // Template method: each activity implements its own ExecuteActivity
        public void RunActivity()
        {
            Start();
            ExecuteActivity(_durationSeconds);
            End(_durationSeconds);
        }

        // Each derived class provides the actual activity behavior
        protected abstract void ExecuteActivity(int durationSeconds);

        // --- Shared UI pieces ---
        private void ShowStartingMessage()
        {
            Console.WriteLine($"=== {_name} ===\n");
            Console.WriteLine(_description + "\n");
        }

        private int PromptForDuration()
        {
            int seconds;
            while (true)
            {
                Console.Write("Enter duration in seconds (e.g. 30): ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out seconds) && seconds > 0)
                {
                    return seconds;
                }
                Console.WriteLine("Please enter a positive whole number.\n");
            }
        }

        private void PrepareToBegin()
        {
            Console.WriteLine("\nGet ready to begin...");
            ShowSpinner(3);          // pause for several seconds with spinner
            Console.WriteLine();
        }

        protected void ShowEndingMessage(int durationSeconds)
        {
            Console.WriteLine("\nWell done!");
            ShowSpinner(3);
            Console.WriteLine($"\nYou have completed the activity: {_name}");
            Console.WriteLine($"Duration: {durationSeconds} seconds.");
            ShowSpinner(3);
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey(true);
        }

        // Spinner animation for X seconds
        protected void ShowSpinner(int seconds)
        {
            char[] spinner = new char[] { '|', '/', '-', '\\' };
            Stopwatch sw = Stopwatch.StartNew();
            int idx = 0;
            while (sw.Elapsed.TotalSeconds < seconds)
            {
                Console.Write(spinner[idx++ % spinner.Length]);
                Thread.Sleep(200);
                Console.Write("\b");
            }
            sw.Stop();
        }

        // Countdown animation (shows numbers)
        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i >= 1; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b"); // remove the digit (works okay for small durations)
            }
        }
    }

    // Breathing activity
    class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base("Breathing Activity",
                  "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
        { }

        protected override void ExecuteActivity(int durationSeconds)
        {
            Console.WriteLine("Follow the prompts: Breathe in... Breathe out...");
            Console.WriteLine();

            Stopwatch sw = Stopwatch.StartNew();
            bool breatheIn = true;
            // We'll show 4-second inhale / 4-second exhale cycles (but adjust to fit time left)
            while (sw.Elapsed.TotalSeconds < durationSeconds)
            {
                if (breatheIn)
                {
                    Console.Write("Breathe in...");
                    // countdown 4 seconds or the remaining seconds if less
                    int wait = (int)Math.Min(4, durationSeconds - sw.Elapsed.TotalSeconds);
                    if (wait <= 0) break;
                    Console.Write(" ");
                    ShowCountdown(wait);
                    Console.WriteLine();
                }
                else
                {
                    Console.Write("Breathe out...");
                    int wait = (int)Math.Min(4, durationSeconds - sw.Elapsed.TotalSeconds);
                    if (wait <= 0) break;
                    Console.Write(" ");
                    ShowCountdown(wait);
                    Console.WriteLine();
                }
                breatheIn = !breatheIn;
            }

            sw.Stop();
        }
    }

    // Reflection activity
    class ReflectionActivity : Activity
    {
        private List<string> _prompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless.",
            "Think of a time when you learned from a failure and kept going."
        };

        private List<string> _questions = new List<string>()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity()
            : base("Reflection Activity",
                  "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
        { }

        protected override void ExecuteActivity(int durationSeconds)
        {
            // Show a random prompt
            string prompt = _prompts[_rnd.Next(_prompts.Count)];
            Console.WriteLine("\nConsider the following prompt:\n");
            Console.WriteLine($"--- {prompt} ---\n");
            Console.WriteLine("When you have something in mind, press Enter to begin reflecting on questions.");
            Console.ReadLine();

            Stopwatch sw = Stopwatch.StartNew();

            // Randomize question order by picking random each time
            while (sw.Elapsed.TotalSeconds < durationSeconds)
            {
                string q = _questions[_rnd.Next(_questions.Count)];
                Console.WriteLine("\n> " + q);
                // Pause for 8 seconds with spinner (or less if near the end)
                int wait = (int)Math.Min(8, durationSeconds - sw.Elapsed.TotalSeconds);
                if (wait <= 0) break;
                ShowSpinner(wait);
            }

            sw.Stop();
        }
    }

    // Listing activity (user lists as many items as possible)
    class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt a sense of peace this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
            : base("Listing Activity",
                  "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
        { }

        protected override void ExecuteActivity(int durationSeconds)
        {
            string prompt = _prompts[_rnd.Next(_prompts.Count)];
            Console.WriteLine("\nList as many responses as you can to the prompt:");
            Console.WriteLine($"--- {prompt} ---\n");
            Console.WriteLine("You will have a few seconds to think, then type items pressing Enter after each one.");
            Console.Write("Starting in: ");
            ShowCountdown(5);
            Console.WriteLine("\nBegin now! (Press Enter after each item)");

            List<string> items = new List<string>();
            Stopwatch sw = Stopwatch.StartNew();

            StringBuilder current = new StringBuilder();

            // We'll read input in a non-blocking way using Console.KeyAvailable
            while (sw.Elapsed.TotalSeconds < durationSeconds)
            {
                while (Console.KeyAvailable && sw.Elapsed.TotalSeconds < durationSeconds)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        string item = current.ToString().Trim();
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            items.Add(item);
                            Console.WriteLine(item); // echo the entered item
                        }
                        else
                        {
                            Console.WriteLine(); // just move to next line
                        }
                        current.Clear();
                    }
                    else if (key.Key == ConsoleKey.Backspace)
                    {
                        if (current.Length > 0)
                        {
                            // Remove last char from buffer and erase from console
                            current.Length--;
                            Console.Write("\b \b");
                        }
                    }
                    else if (!char.IsControl(key.KeyChar))
                    {
                        current.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                    // small sleep to avoid busy waiting
                    Thread.Sleep(10);
                }

                // short sleep to avoid busy-looping while waiting for time to pass
                Thread.Sleep(50);
            }

            // If user was typing something when time expired, add it
            if (current.Length > 0)
            {
                string leftover = current.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(leftover))
                {
                    items.Add(leftover);
                    Console.WriteLine(); // move to next line
                }
            }

            sw.Stop();

            Console.WriteLine($"\nTime's up! You listed {items.Count} item{(items.Count == 1 ? "" : "s")}.");
            if (items.Count > 0)
            {
                Console.WriteLine("Here's what you listed (first 10 shown):");
                for (int i = 0; i < Math.Min(10, items.Count); i++)
                {
                    Console.WriteLine($" {i + 1}. {items[i]}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Mindfulness App";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Mindfulness Activities ====\n");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit\n");
                Console.Write("Select an option (1-4): ");

                string choice = Console.ReadLine();
                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "4":
                        Console.WriteLine("Goodbye — take care!");
                        Thread.Sleep(800);
                        return;
                    default:
                        Console.WriteLine("Invalid selection. Press any key to try again.");
                        Console.ReadKey(true);
                        continue;
                }

                if (activity != null)
                {
                    activity.RunActivity();
                }
            }
        }
    }
}
