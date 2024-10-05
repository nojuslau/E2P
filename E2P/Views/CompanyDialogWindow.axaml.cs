using Avalonia.ReactiveUI;
using E2P.ViewModels;
using ReactiveUI;
using System;

namespace E2P.Views;

public partial class CompanyDialogWindow : ReactiveWindow<CompanyDialogViewModel>
{
    public CompanyDialogWindow()
    {
        InitializeComponent();

        this.WhenActivated(action => action(ViewModel!.CreateNewCompanyCommand.Subscribe(Close)));
    }
}