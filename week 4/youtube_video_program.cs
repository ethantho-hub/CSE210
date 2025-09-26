using System;
using System.Collections.Generic;

namespace YouTubeTracker
{
    public class Comment
    {
        public string Author { get; set; }
        public string Text { get; set; }

        public Comment(string author, string text)
        {
            Author = author;
            Text = text;
        }
    }

    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthInSeconds { get; set; }

        private List<Comment> comments = new List<Comment>();

        public Video(string title, string author, int lengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
        }

        public void AddComment(string author, string text)
        {
            comments.Add(new Comment(author, text));
        }

        public int GetNumberOfComments() => comments.Count;
        public IReadOnlyList<Comment> GetComments() => comments.AsReadOnly();

        public string GetFormattedLength()
        {
            int m = LengthInSeconds / 60;
            int s = LengthInSeconds % 60;
            return $"{m}:{s:D2}"; // mm:ss
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create videos and add comments
            var videos = new List<Video>();

            var video1 = new Video("C# Basics", "Alice", 300);
            video1.AddComment("John", "Great explanation!");
            video1.AddComment("Mary", "Very helpful, thanks!");
            video1.AddComment("Sam", "I learned a lot.");
            videos.Add(video1);

            var video2 = new Video("OOP Concepts", "Bob", 450);
            video2.AddComment("Lisa", "Nice breakdown of OOP.");
            video2.AddComment("Tom", "Can you make a follow-up video?");
            video2.AddComment("Harry", "Clear and concise.");
            videos.Add(video2);

            var video3 = new Video("Advanced C#", "Charlie", 600);
            video3.AddComment("Anna", "This was challenging but useful.");
            video3.AddComment("Mike", "Loved the examples.");
            video3.AddComment("Zoe", "Could you explain LINQ next?");
            videos.Add(video3);

            // Display videos
            foreach (var v in videos)
            {
                Console.WriteLine($"Title: {v.Title}");
                Console.WriteLine($"Author: {v.Author}");
                Console.WriteLine($"Length: {v.GetFormattedLength()} ({v.LengthInSeconds} seconds)");
                Console.WriteLine($"Number of Comments: {v.GetNumberOfComments()}");
                Console.WriteLine("Comments:");
                foreach (var c in v.GetComments())
                {
                    Console.WriteLine($" - {c.Author}: {c.Text}");
                }
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
