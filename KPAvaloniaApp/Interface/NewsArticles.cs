using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KPAvalonia
{
    public class NewsArticles
    {
        public NewsArticles(string title, string author, string description, string url, IBitmap image, string publishedAt)
        {
            Title = title;
            Author = author;
            Description = description;
            Url = url;
            Image = image;
            PublishedAt = publishedAt;
        }

        public string Title { get; }
        public string Author { get; }
        public string Description { get; }
        public string Url { get; }
        public Image Image { get; }
        public string PublishedAt { get; }
    }
}
