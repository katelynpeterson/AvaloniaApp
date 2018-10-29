namespace service{
    internal interface IFileService
{
    string GetSaveLocation();
    void SaveOutput(string output);
}
}