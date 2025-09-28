using System;
using System.Collections.Generic;

namespace YouTubeTracker
{
    // Represents a single comment on a video
    public class Comment
    {
        public string CommenterName { get; set; }
        public string Text { get; set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }
    }

    // Represents a YouTube video with comments
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int LengthSeconds { get; set; }
        private List<Comment> comments = new List<Comment>();

        public Video(string title, string author, int lengthSeconds)
        {
            Title = title;
            Author = author;
            LengthSeconds = lengthSeconds;
        }

        public void AddComment(Comment comment)
        {
            comments.Add(comment);
        }

        public int GetCommentCount()
        {
            return comments.Count;
        }

        public List<Comment> GetComments()
        {
            return comments;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create sample videos
            var video1 = new Video("C# Basics Tutorial", "CodeAcademy", 600);
            var video2 = new Video("Top 10 Space Facts", "ScienceWorld", 480);
            var video3 = new Video("Epic Guitar Solo", "MusicMaster", 300);

            // Add comments to video1
            video1.AddComment(new Comment("Alice", "Great tutorial!"));
            video1.AddComment(new Comment("Bob", "Very clear explanations."));
            video1.AddComment(new Comment("Charlie", "Helped me a lot, thanks!"));

            // Add comments to video2
            video2.AddComment(new Comment("Diana", "Space is so fascinating."));
            video2.AddComment(new Comment("Ethan", "Loved the visuals!"));
            video2.AddComment(new Comment("Fiona", "I learned something new today."));

            // Add comments to video3
            video3.AddComment(new Comment("George", "Absolutely amazing solo."));
            video3.AddComment(new Comment("Hannah", "You rock!"));
            video3.AddComment(new Comment("Ian", "Practicing this riff right now."));
            video3.AddComment(new Comment("Jack", "Encore please!"));

            // Store videos in a list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Display all details
            foreach (var v in videos)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"Title: {v.Title}");
                Console.WriteLine($"Author: {v.Author}");
                Console.WriteLine($"Length: {v.LengthSeconds} seconds");
                Console.WriteLine($"Number of Comments: {v.GetCommentCount()}");
                Console.WriteLine("Comments:");
                foreach (var c in v.GetComments())
                {
                    Console.WriteLine($"   {c.CommenterName}: {c.Text}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
