using ReactiveUI;
using System.Windows.Input;

namespace E2P.ViewModels;
public class ViewModelBase : ReactiveObject
{
    public ICommand NavigateToCompaniesCommand { get; }
    public ICommand NavigateToExcelFilesCommand { get; }
    public ICommand NavigateToSettingsCommand { get; }

    public ViewModelBase()
    {
        NavigateToCompaniesCommand = ReactiveCommand.Create(NavigateToCompanies);
        NavigateToExcelFilesCommand = ReactiveCommand.Create(NavigateToExcelFiles);
        NavigateToSettingsCommand = ReactiveCommand.Create(NavigateToSettings);
    }

    private void NavigateToCompanies()
    {
        // Logic to navigate to the Companies view
        var companiesWindow = new Views.MainWindow();
        companiesWindow.Show();
        CloseCurrentWindow();
    }

    private void NavigateToExcelFiles()
    {
        // Logic to navigate to the Excel Files view
        //var excelFilesWindow = new Views.CompanyExcelFilesWindow();
        //excelFilesWindow.Show();
        //CloseCurrentWindow();
    }

    private void NavigateToSettings()
    {
        // Logic to navigate to the Settings view
        //var settingsWindow = new Views.SettingsWindow(); // Assuming you have a SettingsWindow
        //settingsWindow.Show();
        //CloseCurrentWindow();
    }

    private void CloseCurrentWindow()
    {
        // Close the current active window
        //var currentWindow = Views.App.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
        //currentWindow?.MainWindow.Close();
    }
}
