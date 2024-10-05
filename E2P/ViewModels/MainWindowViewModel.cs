using Avalonia.Controls;
using E2P.Services;
using E2P.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Reactive;

namespace E2P.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IScreen
{
    private IServiceProvider _serviceProvider;
    private UserControl _currentPage;
    private string _headerTitle;
    public RoutingState Router { get; } = new RoutingState();
    public ReactiveCommand<Unit, Unit> NavigateToCompaniesCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToExcelFilesCommand { get; }

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CurrentPage = new CompaniesUserControl(_serviceProvider.GetRequiredService<CompanyService>());
        var companyService = _serviceProvider.GetRequiredService<CompanyService>();
        HeaderTitle = "Companies";

        // Initialize the navigation commands
        //NavigateToCompaniesCommand = ReactiveCommand.CreateFromObservable(
        //    () => Router.Navigate.Execute(new CompaniesViewModel(this)));

        //NavigateToExcelFilesCommand = ReactiveCommand.CreateFromObservable(
        //    () => Router.Navigate.Execute(new ExcelFilesViewModel(this)));
    }

    public UserControl CurrentPage
    {
        get => _currentPage;
        set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public string HeaderTitle
    {
        get => _headerTitle;
        set => this.RaiseAndSetIfChanged(ref _headerTitle, value);
    }

    private void NavigateToCompanies()
    {
        CurrentPage = new CompaniesUserControl(_serviceProvider.GetRequiredService<CompanyService>());
        HeaderTitle = "Companies";
    }

    private void NavigateToExcelFiles()
    {
        CurrentPage = new ExcelFilesUserControl();
        HeaderTitle = "Excel Files";
    }
}
