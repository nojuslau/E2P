namespace E2P.AppSettingsModels;
public class ApplicationSettings
{
    public string DatabaseDirectoryPath { get; set; }
    public string DatabaseFilePath { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
}
