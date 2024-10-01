namespace E2P.ViewModels;
public class ExcelFileViewModel : ViewModelBase
{
    public string FilePath { get; }

    public ExcelFileViewModel(string filePath)
    {
        FilePath = filePath;

        // Here you can add logic to open the Excel file and process its content.
        // For example, you could parse the Excel file here using a library like ClosedXML or EPPlus.
    }
}