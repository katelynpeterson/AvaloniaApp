using System;
using Avalonia.Controls;
using System.IO;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Extensions.Configuration;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Collections.ObjectModel;
using KPAvalonia;

namespace Data
{
    public class DefaultDataService : IDataService
    {
        public bool FileExists(string imageFile)
        {
            return File.Exists(imageFile);
        }

        public async Task<string> FindFileAsync()
        {
            var openFileDialog = new OpenFileDialog()
            {
                AllowMultiple = false,
                Title = "Select a picture"
            };
            var pathArray = await openFileDialog.ShowAsync();

            if ((pathArray?.Length ?? 0) > 0)
                return pathArray[0];
            return null;
        }

        public async Task<ObservableCollection<NewsArticles>> GetNews(string searchQuery)
        {
            results = 0;
            var News = new ObservableCollection<NewsArticles>();
            var ResultsAndArticles = new ObservableCollection<Object>();
            var client = new NewsAPI.NewsApiClient(getNewsServiceAPIToken());
            var newsArticlesResponse = client.GetEverything(new EverythingRequest {
                Q = searchQuery,
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = new DateTime(2018, 1, 25)

            });
            if (newsArticlesResponse.Status == Statuses.Ok)
            {
                // total results found
                results = $"{newsArticlesResponse.TotalResults}";

                // here's the first 20
                foreach (var article in newsArticlesResponse.Articles)
                {
                    News.Add(new NewsArticles(article.Title, article.Author, article.Description, article.Url, article.UrlToImage, article.PublishedAt));
                }
            }
            return News;
        }

        private string results;
        public string GetSearchResults()
        {
            return $"Search results: {results}";
        }

        private static string getNewsServiceAPIToken()
        {
            var configurationBuilder = new ConfigurationBuilder()
                            .AddUserSecrets("newsServiceAPIToken");
            var config = configurationBuilder.Build();
            var weatherAppId = config["newsServiceAppId"];
            return weatherAppId;
        }
    }
}
