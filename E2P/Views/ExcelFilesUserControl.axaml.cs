using Avalonia.Controls;
using Avalonia.ReactiveUI;
using E2P.ViewModels;

namespace E2P.Views;

public partial class ExcelFilesUserControl : ReactiveUserControl<ExcelFilesViewModel>
{
    public ExcelFilesUserControl()
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