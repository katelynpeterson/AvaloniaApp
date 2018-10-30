using Avalonia.Controls;

namespace Service{
class MyFileService : IFileService
{

        string IFileService.GetSaveLocation()
    {
            var openFileDialog = new OpenFileDialog();
            return "something";
    }

    void IFileService.SaveOutput(string output)
    {
        
    }
}
}