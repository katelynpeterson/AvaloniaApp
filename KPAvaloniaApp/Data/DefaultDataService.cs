using System;
using Avalonia.Controls;
using System.IO;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.Extensions.Configuration;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Collections.ObjectModel;
using NewsAPI;
using Hassie.NET.API.NewsAPI.Client;
using Hassie.NET.API.NewsAPI.Models;
using Hassie.NET.API.NewsAPI.API.v2;

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
            results = $"{0}";
            var News = new ObservableCollection<NewsArticles>();

            //HassieNewsAPI
            INewsClient client = new ClientBuilder(){
                ApiKey = "42b3db2e64f3417b9a0f05c631e544e3"
            }.Build();
            try
            {
                INewsArticles newsArticlesResponse = await client.GetTopHeadlines(new TopHeadlinesBuilder()
           // .WithCountryQuery(Country.US)
            //.WithLanguageQuery(Language.EN)
            .WithSourcesQuery(Hassie.NET.API.NewsAPI.API.v2.Source.ABC_NEWS)
            .Build());



                //NewsAPI
                //var client = new NewsApiClient("42b3db2e64f3417b9a0f05c631e544e3");
                //var newsArticlesResponse = client.GetEverything(new EverythingRequest {
                //    Q = searchQuery,
                //    SortBy = SortBys.Popularity,
                //    Language = Languages.EN,
                //    From = new DateTime(2018, 1, 25)

                //});
                //if (newsArticlesResponse.Status == Statuses.Ok)
                //{
                //    // total results found
                results = $"{newsArticlesResponse.Count}";

                //    // here's the first 20
                //    foreach (var article in newsArticlesResponse.Articles)
                //    {
                //        News.Add(new NewsArticles(article.Title??"No Result", article.Author ?? "No Result", article.Description ?? "No Result", article.Url ?? "No Result", article.UrlToImage ?? "No Result", article.PublishedAt ?? null));
                //    }
                //}

                foreach (var article in newsArticlesResponse)
            {
                News.Add(new NewsArticles(article.Title ?? "No Result", article.Author ?? "No Result", article.Description ?? "No Result", article.URL ?? "No Result", article.ImageURL ?? "No Result", article.PublishTime, article.SourceName ?? "No Result"));
            }
            }
            catch (Exception ex)
            {
                Console.Write($"Error {ex.Message}");
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
                            .AddUserSecrets("newsServiceAppId");
            var config = configurationBuilder.Build();
            var weatherAppId = config["newsServiceAppId"];
            return weatherAppId;
        }
    }
}
