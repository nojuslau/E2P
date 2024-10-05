using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using E2P.Models;
using E2P.Services;
using E2P.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace E2P.Views;

public partial class CompaniesUserControl : ReactiveUserControl<CompaniesViewModel>
{
    public CompaniesUserControl(CompanyService companyService)
    {
        InitializeComponent();
        DataContext = new CompaniesViewModel(companyService);
        this.WhenActivated(action =>
        {
            if (ViewModel != null)
            {
                action(ViewModel.ShowDialog.RegisterHandler(DoShowDialogAsync));
            }
        });
    }
    private async Task DoShowDialogAsync(InteractionContext<CompanyDialogViewModel,
                                                Company?> interaction)
    {
        var dialog = new CompanyDialogWindow();
        dialog.DataContext = interaction.Input;

        // Get the parent window
        var parentWindow = this.GetVisualRoot() as Window;

        if (parentWindow != null)
        {
            var result = await dialog.ShowDialog<Company?>(parentWindow);
            interaction.SetOutput(result);
        }
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is CompaniesViewModel viewModel && sender is DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem is Company selectedCompany)
            {
                var excelFilesWindow = new CompanyExcelFilesWindow
                {
                    DataContext = new ExcelFilesViewModel(selectedCompany)
                };

                excelFilesWindow.Show();
            }
        }
    }
}