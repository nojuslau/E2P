using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using E2P.Models;
using E2P.Services;
using E2P.Views;
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
        public ICommand NavigateBackCommand { get; }

        public CompanyExcelFilesViewModel(Company company)
        {
            Company = company;
            FilteredExcelFiles = new ObservableCollection<ExcelFile>(Company.ExcelFiles);
            NavigateBackCommand = ReactiveCommand.Create(NavigateBack);

            AddNewExcelFile = ReactiveCommand.CreateFromTask(async () =>
            {
                var topLevel = Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;
                if (topLevel != null && topLevel.StorageProvider.CanOpen)
                {
                    var options = new FilePickerOpenOptions
                    {
                        AllowMultiple = false,
                        FileTypeFilter = new List<FilePickerFileType>
                        {
                            new FilePickerFileType("Excel Files")
                            {
                                Patterns = new[] { "*.xlsx", "*.xls" }
                            }
                        }
                    };

                    var result = await topLevel.StorageProvider.OpenFilePickerAsync(options);
                    if (result.Any())
                    {
                        var selectedFilePath = result.First().Path.LocalPath;
                        OpenExcelFileWindow(selectedFilePath);
                    }
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

        private void OpenExcelFileWindow(string filePath)
        {
            var excelFileViewModel = new ExcelFileViewModel(filePath);
            var excelFileWindow = new ExcelFileWindow
            {
                DataContext = excelFileViewModel
            };
            excelFileWindow.Show();
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

            this.RaisePropertyChanged(nameof(FilteredExcelFiles));
        }

        // Navigate back to the MainWindow
        private void NavigateBack()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            
        }
    }
}
