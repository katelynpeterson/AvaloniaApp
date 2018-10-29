class ViewModel{
    public ViewModel(IFileService fileService){
        GetPathCommand_Execute(){
            FilePath = fileService.GetSaveLocation();
        }
    }
}