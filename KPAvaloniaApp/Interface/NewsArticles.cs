using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Interfaces
{
    public class NewsArticles
    {
        public NewsArticles(string title, string author, string description, string url, string image, DateTimeOffset? publishedAt, string sourceName)
        {
            Title = title;
            Author = author;
            Description = description;
            Url = url;
            Image = image;
            PublishedAt = publishedAt;
            SourceName = sourceName;
        }

        public string Title { get; }
        public string Author { get; }
        public string Description { get; }
        public string Url { get; }
        public string Image { get; }
        public DateTimeOffset? PublishedAt { get; }
        public string SourceName { get; }
    }
}
