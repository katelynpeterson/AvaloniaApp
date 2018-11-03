using KPAvalonia;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataService
    {
        bool FileExists(string imageFile);
        Task<string> FindFileAsync();
        Task<ObservableCollection<NewsArticles>> GetNews(string searchQuery);
        string GetSearchResults();
    }
}
