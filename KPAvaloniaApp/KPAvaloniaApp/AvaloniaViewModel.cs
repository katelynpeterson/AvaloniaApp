using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KPAvaloniaApp
{
    class AvaloniaViewModel : INotifyPropertyChanged
    {
        public AvaloniaViewModel() : this(new DefaultDataService())
        { }

        public MainViewModel(IDataService data)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));
            
        }

        private string pathToPicture;
        public string PathToPicture
        {
            get => pathToPicture;
            set
            {
                pathToPicture=value;
                OnPropertyChanged(nameof(PathToPicture));
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

        private SimpleCommand loadImage;
        public SimpleCommand LoadImage => loadImage ?? (loadImage = new SimpleCommand(
        () => !IsBusy && data.FileExists(PathToPicture), //can execute
        async () => //execute
        {
            Output = "Loading...";
            //IsBusy = true;
            //foreach (var p in await data.GetPeopleFromGedcomAsync(PathToPicture))
            //    People.Add(p);
            //Output = $"We found {People.Count} people in {PathToPicture}!";
            IsBusy = false;
        }));

        private SimpleCommand findFile;
        public SimpleCommand FindFile => findFile ?? (findFile = new SimpleCommand(
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
