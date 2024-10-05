using ReactiveUI;
using System.Reactive;

namespace E2P.ViewModels;
public class ViewModelBase : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> NavigateToCompaniesCommand { get; set; }
    public ReactiveCommand<Unit, Unit> NavigateToExcelFilesCommand { get; set; }
    public ReactiveCommand<Unit, Unit> NavigateToSettingsCommand { get; }
}
