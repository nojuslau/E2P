using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace E2P.Views;

public partial class TopbarTemplate : UserControl
{
    private CancellationTokenSource? _cts;

    public TopbarTemplate()
    {
        InitializeComponent();

        BreadcrumbBar.Resources["BreadcrumbBarChevronPadding"] = new Thickness(4, 4, 4, 0);
        BreadcrumbBar.Resources["BreadcrumbBarItemFontWeight"] = FontWeight.SemiBold;
        BreadcrumbBar.Resources["BreadcrumbBarItemThemeFontSize"] = 24d;
        BreadcrumbBar.Resources["BreadcrumbBarChevronFontSize"] = 16d;

        BreadcrumbBar.ItemClicked += BreadcrumbBar_ItemClicked;
    }

    private async void BreadcrumbBar_ItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        InfoBar.Content = $"Index: {args.Index}, Item: {args.Item}";
        InfoBar.IsOpen = true;
        try
        {
            await Task.Delay(5000, _cts.Token);
            InfoBar.IsOpen = false;
        }
        catch (OperationCanceledException)
        {
        }
    }
}