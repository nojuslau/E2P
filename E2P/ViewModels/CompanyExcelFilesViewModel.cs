using E2P.Models;
using E2P.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace E2P.ViewModels
{
    public class CompanyExcelFilesViewModel : ViewModelBase
    {
        private string _searchQuery;
        private ExcelFile _selectedExcelFile;

        public Company Company { get; set; }
        public List<ExcelFile> ExcelFiles { get; set; } = new();
        public ObservableCollection<ExcelFile> FilteredExcelFiles { get; set; }
        public ICommand AddNewExcelFile { get; }

        public CompanyExcelFilesViewModel(Company company)
        {
            Company = company;
            FilteredExcelFiles = new ObservableCollection<ExcelFile>(Company.ExcelFiles);

            AddNewExcelFile = ReactiveCommand.CreateFromTask(async () =>
            {
                var company = new ExcelFile();
                var result = await ShowDialog.Handle(company);

                if (result != null)
                {
                    await _companyService.CreateAsync(result);
                    Companies.Add(result);
                    FilterCompanies();
                }
            });
        }

        public ExcelFile SelectedExcelFile
        {
            get => _selectedExcelFile;
            set => this.RaiseAndSetIfChanged(ref _selectedExcelFile, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                this.RaiseAndSetIfChanged(ref _searchQuery, value);
                FilterCompanyExcelFiles();
            }
        }

        private void FilterCompanyExcelFiles()
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

            // Notify that the FilteredExcelFiles has changed so the DataGrid can update
            this.RaisePropertyChanged(nameof(FilteredExcelFiles));
        }
    }
}
