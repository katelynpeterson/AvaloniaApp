namespace Service
{
    internal interface IFileService
    {
        string GetSaveLocation();
        void SaveOutput(string output);
    }
}