using Avalonia.ReactiveUI;
using E2P.ViewModels;

namespace E2P.Views;

public partial class ExcelFileWindow : ReactiveWindow<ExcelFileViewModel>
{
    public ExcelFileWindow()
    {
        InitializeComponent();
    }
}