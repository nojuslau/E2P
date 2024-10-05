using Reactive.Bindings;
using ReactiveUI;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Windows.Input;

namespace E2P.ViewModels;

public class ViewModelBase : ReactiveObject
{
    public ReactiveCollection<BreadcrumbViewModel> Breadcrumbs { get; }

    [Required]
    public ReactiveProperty<string> Text { get; } = new();
    public ReactiveCommand<string> Add { get; }
    public ICommand NavigateToCompaniesCommand { get; }
    public ICommand NavigateToExcelFilesCommand { get; }
    public ICommand NavigateToSettingsCommand { get; }

    public ViewModelBase()
    {
        Breadcrumbs = new()
        {
            new("Home"),
            new("Documents"),
            new("Folder1"),
        };

        Text.SetValidateAttribute(() => Text);
        Add = new ReactiveCommand<string>(Text.ObserveHasErrors.Select(b => !b));
        Add.Do(x => Breadcrumbs.Add(new BreadcrumbViewModel(x)))
            .Subscribe(_ => Text.Value = "");
    }
}
