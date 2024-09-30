using E2P.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace E2P.ViewModels;
public class CompanyExcelFilesViewModel : ViewModelBase
{
    private string _searchQuery;
    private ExcelFile _selectedExcelFile;

    public Company Company { get; set; }
    public List<ExcelFile> ExcelFiles { get; set; } = new();
    public ObservableCollection<ExcelFile> FilteredExcelFiles { get; set; }

    public CompanyExcelFilesViewModel(Company company)
    {
        Company = company;
        FilteredExcelFiles = new ObservableCollection<ExcelFile>(Company.ExcelFiles);
    }

    public ExcelFile SelectedExcelFile
    {
        get => _selectedExcelFile;
        set
        {
            //this.RaiseAndSetIfChanged(ref _selectedCompany, value);
            //if (_selectedCompany != null)
            //{
            //    ViewCompanyCommand.Execute(_selectedCompany);
            //}
        }
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            this.RaiseAndSetIfChanged(ref _searchQuery, value);
            FilterComapnyExcelFiles();
        }
    }

    private void FilterComapnyExcelFiles()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery))
        {
            FilteredExcelFiles = new ObservableCollection<ExcelFile>(Company.ExcelFiles);
        }
        else
        {
            FilteredExcelFiles = new ObservableCollection<ExcelFile>(
                Company.ExcelFiles.Where(c => c.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)));
        }
        this.RaisePropertyChanged(nameof(FilteredExcelFiles));
    }
}