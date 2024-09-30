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

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly CompanyService _companyService;
    private Company _selectedCompany;
    private string _searchQuery;

    public event Action<Company> CompanySelected;

    public Interaction<CompanyViewModel, Company?> ShowDialog { get; }
    public ObservableCollection<Company> FilteredCompanies { get; set; }
    public List<Company> Companies { get; set; } = new();
    public CompanyService CompanyService { get; }
    public ICommand CreateNewCompanyCommand { get; }

    public MainWindowViewModel(CompanyService companyService)
    {
        _companyService = companyService;
        ShowDialog = new Interaction<CompanyViewModel, Company?> ();
        LoadCompanies();

        CreateNewCompanyCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var company = new CompanyViewModel();
            var result = await ShowDialog.Handle(company);

            if (result != null)
            {
                await _companyService.CreateAsync(result);
                Companies.Add(result);
                FilterCompanies();
            }
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
            CompanySelected?.Invoke(_selectedCompany);
        }
    }
}
