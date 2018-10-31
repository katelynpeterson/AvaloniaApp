using System;
using Avalonia.Controls;
using System.IO;
using System.Threading.Tasks;
using Interfaces;

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
    }
}
