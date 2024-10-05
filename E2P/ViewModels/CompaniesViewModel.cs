using Avalonia.Controls;
using Avalonia.Platform;
using E2P.Models;
using E2P.Services;
using E2P.Views;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace E2P.ViewModels;
public class CompaniesViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly CompanyService _companyService;
    private Company _selectedCompany;
    private string _searchQuery;
    private UserControl _currentPage;
    private string _headerTitle;

    public IScreen HostScreen { get; }
    public string? UrlPathSegment { get; } = "CompaniesPage";
    public Interaction<CompanyDialogViewModel, Company?> ShowDialog { get; }
    public ObservableCollection<Company> FilteredCompanies { get; set; }
    public event Action<Company> CompanySelected;
    public List<Company> Companies { get; set; } = new();
    public ICommand CreateNewCompanyCommand { get; }

    public ReactiveCommand<Unit, Unit> NavigateToCompaniesCommand { get; }
    public ReactiveCommand<Unit, Unit> NavigateToExcelFilesCommand { get; }
    public ReactiveCommand<Company, Unit> SelectCompanyCommand { get; }

    public CompaniesViewModel(CompanyService companyService, IScreen screen)
    {
        HostScreen = screen;
        _companyService = companyService;
        ShowDialog = new Interaction<CompanyDialogViewModel, Company?>();
        LoadCompanies();

        CreateNewCompanyCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var company = new CompanyDialogViewModel();
            var result = await ShowDialog.Handle(company);

            if (result != null)
            {
                await _companyService.CreateAsync(result);
                Companies.Add(result);
                FilterCompanies();
            }
        });

        SelectCompanyCommand = ReactiveCommand.Create<Company>(company =>
        {
            SelectedCompany = company;
            NavigateToExcelFiles();
        });

        NavigateToExcelFilesCommand = ReactiveCommand.Create(NavigateToExcelFiles);
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            this.RaiseAndSetIfChanged(ref _searchQuery, value);
            FilterCompanies();
        }
    }
    public Company SelectedCompany
    {
        get => _selectedCompany;
        set
        {
            CompanySelected?.Invoke(_selectedCompany);
        }
    }

    private void FilterCompanies()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            FilteredCompanies = new ObservableCollection<Company>(Companies);
        }
        else
        {
            FilteredCompanies = new ObservableCollection<Company>(
                Companies.Where(c => c.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)));
        }
        this.RaisePropertyChanged(nameof(FilteredCompanies));
    }

    private async void LoadCompanies()
    {
        Companies = (await _companyService.GetAllAsync()).ToList();
        FilteredCompanies = new ObservableCollection<Company>(Companies);
    }

    private void NavigateToExcelFiles()
    {
        // Logic to navigate to ExcelFiles view
        CurrentPage = new ExcelFilesUserControl();
        HeaderTitle = "Excel Files";
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
}
