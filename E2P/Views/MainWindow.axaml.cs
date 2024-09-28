using Avalonia.Controls;
using Avalonia.ReactiveUI;
using E2P.Models;
using E2P.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;

namespace E2P.Views;
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action =>
        {
            if (ViewModel != null)
            {
                action(ViewModel.ShowDialog.RegisterHandler(DoShowDialogAsync));
            }
        });
    }

    private async Task DoShowDialogAsync(InteractionContext<CreateCompanyViewModel, CompanyViewModel?> interaction)
    {
        //var dialog = new CreateCompanyViewModel();
        //dialog.DataContext = interaction.Input;

        //var result = await dialog.ShowDialog<CompanyViewModel?>(this);
        //interaction.SetOutput(result);
    }

    private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (DataContext is MainWindowViewModel viewModel && sender is DataGrid dataGrid)
        {
            if (dataGrid.SelectedItem is Company selectedCompany)
            {
                //viewModel.ViewCompanyCommand.Execute(selectedCompany);
            }
        }
    }
}