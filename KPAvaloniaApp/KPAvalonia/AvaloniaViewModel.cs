using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Data;
using Interfaces;

namespace KPAvalonia
{
    class AvaloniaViewModel : INotifyPropertyChanged
    {
        public AvaloniaViewModel() : this(new DefaultDataService())
        { }

        public AvaloniaViewModel(IDataService data)
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
            //foreach (var p in await data.GetPeopleFromGedcomAsync(PathToPicture))
            //    People.Add(p);
            //Output = $"We found {People.Count} people in {PathToPicture}!";
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

        private Image imageViewer;
        public Image ImageViewer
        {
            get { return imageViewer; }
            set { imageViewer = value;
                OnPropertyChanged(nameof(ImageViewer));
            }
        }

    }
}
