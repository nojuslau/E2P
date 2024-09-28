using E2P.Models;
using ReactiveUI;
using System;
using System.Reactive;

namespace E2P.ViewModels;
public class CreateCompanyViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, CreateCompanyViewModel?> CreateNewCompanyCommand { get; }
    private Company? _createdCompany;

    public CreateCompanyViewModel()
    {
        CreateNewCompanyCommand = ReactiveCommand.Create(() =>
        {
            return _createdCompany;
        });
    }
}
