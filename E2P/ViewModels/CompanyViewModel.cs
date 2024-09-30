using E2P.Models;
using ReactiveUI;
using System.Reactive;

namespace E2P.ViewModels;
public class CompanyViewModel : ViewModelBase
{
    public Company Company { get; set; }
    public ReactiveCommand<Unit, Company?> CreateNewCompanyCommand { get; }
    public CompanyViewModel() 
    {
        Company = new Company();

        CreateNewCompanyCommand = ReactiveCommand.Create(() =>
        {
            return Company;
        });
    }
}
