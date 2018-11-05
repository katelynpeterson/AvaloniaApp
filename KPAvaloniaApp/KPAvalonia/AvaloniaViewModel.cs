using System;
using System.ComponentModel;
using System.Drawing;
using Data;
using Interfaces;
using Avalonia.Data.Converters;
using System.Collections.ObjectModel;

namespace KPAvalonia
{
    public class AvaloniaViewModel : INotifyPropertyChanged
    {
        public AvaloniaViewModel() : this(new DefaultDataService())
        { }

        public AvaloniaViewModel(IDataService data)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));
            Articles = new ObservableCollection<NewsArticles>();
        }

        private string totalNewsResults;
        public string TotalNewsResults
        {
            get => totalNewsResults;
            set
            {
                totalNewsResults = value;
                OnPropertyChanged(nameof(TotalNewsResults));
            }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }



        private MyCommand getNews;
        public MyCommand GetNews => getNews ?? (getNews = new MyCommand(async () =>
        {
            try
            {
                Articles = await data.GetNews(SearchQuery);
                TotalNewsResults = data.GetSearchResults();
                if (Articles.Count > 0)
                    SelectedArticle = Articles[0];
            }
            catch (Exception ex)
            {
                TotalNewsResults = $"Whoops!  Error: {ex.Message}";
            }
        }));

        public ObservableCollection<NewsArticles> Articles { get; private set; }

        private NewsArticles selectedArticle;
        public NewsArticles SelectedArticle
        {
            get => selectedArticle;
            set
            {
                selectedArticle = value;
                OnPropertyChanged(nameof(SelectedArticle));
            }
        }

        private string pathToPicture;
        public string PathToPicture
        {
            get => pathToPicture;
            set
            {
                pathToPicture = value;
                OnPropertyChanged(nameof(PathToPicture));
                LoadImage.RaiseCanExecuteChanged();
            }
        }

        private string output;
        public string Output
        {
            get => output;
            set
            {
                output = value;
                OnPropertyChanged(nameof(Output));
            }
        }

        private readonly IDataService data;

        private MyCommand loadImage;
        public MyCommand LoadImage => loadImage ?? (loadImage = new MyCommand(
        () => !IsBusy && data.FileExists(PathToPicture), //can execute
        async () => //execute
        {
            Output = "Loading...";
            IsBusy = true;
            IsBusy = false;
        }));

        private MyCommand findFile;
        public MyCommand FindFile => findFile ?? (findFile = new MyCommand(
            () => !IsBusy,
            async () =>
            {
                PathToPicture = await data.FindFileAsync();
                LoadImage.RaiseCanExecuteChanged();
            }));

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                LoadImage.RaiseCanExecuteChanged();
                FindFile.RaiseCanExecuteChanged();
            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
