namespace service{
class MyFileService : IFileService
{
    string IFileService.GetSaveLocation()
    {
        System.Windows.Forms.OpenFileDialog openFileDialog;
        this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
    }

    void IFileService.SaveOutput(string output)
    {
        throw new System.NotImplementedException();
    }
}
}