using E2P.Models;
using E2P.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

namespace E2P.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    public Interaction<CreateCompanyViewModel, Company?> ShowDialog { get; }
    public ICommand CreateNewCompanyCommand { get; }
    public CompanyService CompanyService { get; }
    public List<Company> Companies { get; set; } = new();
    public ObservableCollection<Company> FilteredCompanies { get; set; }
    private readonly CompanyService _companyService;
    private Company _selectedCompany;
    private string _searchQuery;

    public MainWindowViewModel(CompanyService companyService)
    {
        _companyService = companyService;
        ShowDialog = new Interaction<CreateCompanyViewModel, Company?> ();
        LoadCompanies();

        CreateNewCompanyCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var company = new Company();

            var result = await ShowDialog.Handle(company);
        });
    }

    private async void LoadCompanies()
    {
        Companies = (await _companyService.GetAllAsync()).ToList();
        FilteredCompanies = new ObservableCollection<Company>(Companies);
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

    public Company SelectedCompany
    {
        get => _selectedCompany;
        set
        {
            //this.RaiseAndSetIfChanged(ref _selectedCompany, value);
            //if (_selectedCompany != null)
            //{
            //    ViewCompanyCommand.Execute(_selectedCompany);
            //}
        }
    }
}
