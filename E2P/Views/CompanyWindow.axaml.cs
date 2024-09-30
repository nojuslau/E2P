using Avalonia.ReactiveUI;
using E2P.ViewModels;
using ReactiveUI;
using System;

namespace E2P;

public partial class CompanyWindow : ReactiveWindow<CompanyViewModel>
{
    public CompanyWindow()
    {
        InitializeComponent();

        this.WhenActivated(action => action(ViewModel!.CreateNewCompanyCommand.Subscribe(Close)));
    }
}