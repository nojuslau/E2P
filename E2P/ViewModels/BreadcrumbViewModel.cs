namespace E2P.ViewModels;
public class BreadcrumbViewModel
{
    public BreadcrumbViewModel(string text)
    {
        Text = text;
    }

    public string Text { get; set; }
}
