using Avalonia.ReactiveUI;
using E2P.ViewModels;
using ReactiveUI;

namespace E2P.Views;
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(disposables => { /* Handle view activations here */ });
    }
}