using Avalonia.Controls;
using Avalonia.ReactiveUI;
using E2P.Models;
using E2P.ViewModels;

namespace E2P;

public partial class CompanyExcelFilesWindow : ReactiveWindow<CompanyExcelFilesViewModel>
{
    public CompanyExcelFilesWindow()
    {
        InitializeComponent();
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //if (DataContext is MainWindowViewModel viewModel && sender is DataGrid dataGrid)
        //{
        //    if (dataGrid.SelectedItem is Company selectedCompany)
        //    {
        //        //viewModel.ViewCompanyCommand.Execute(selectedCompany);
        //    }
        //}
    }
}