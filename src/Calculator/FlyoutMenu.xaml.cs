using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Calculator;

public class FlyoutMenuItem
{
    public FlyoutMenuItem(string title)
    {
        Title = title;
    }
    public string Title { get; set; }
}
public partial class FlyoutMenu : ContentPage
{
    ObservableCollection<FlyoutMenuItem> flyoutMenuItems = new ObservableCollection<FlyoutMenuItem>();
    public ObservableCollection<FlyoutMenuItem> FlyoutMenuItems { get { return flyoutMenuItems; } } //public property that only includes a getter
    public FlyoutMenu()
	{
		InitializeComponent();
		flyoutMenuItems.Add(new FlyoutMenuItem("About"));
        flyoutMenuItems.Add(new FlyoutMenuItem("Basic Calculator"));
        flyoutMenuItems.Add(new FlyoutMenuItem("Scientific Calculator"));
        flyoutMenuItems.Add(new FlyoutMenuItem("Themes"));
        flyoutMenuItems.Add(new FlyoutMenuItem("Exercises"));
        flyoutMenuOptions.ItemsSource = flyoutMenuItems;
    }
}