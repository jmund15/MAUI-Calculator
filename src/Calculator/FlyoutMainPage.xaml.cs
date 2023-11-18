using System.Diagnostics;

namespace Calculator;

public partial class FlyoutMainPage : FlyoutPage
{
    FlyoutMenuItem currItem;
    public FlyoutMainPage()
	{
		InitializeComponent();
        flyoutMenu.flyoutMenuOptions.SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutMenuItem;
        if (item != null && item != currItem)
        {
            Trace.WriteLine("Changed");
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;

            switch (item.Title)
            {
                case "About":
                    Detail = new NavigationPage(new AboutPage());
                    break;
                case "Basic Calculator":
                    Detail = new NavigationPage(new MainPage());
                    break;
                case "Scientific Calculator":
                    Detail = new NavigationPage(new ScientificPage());    
                    break;
                case "Themes":
                    Detail = new NavigationPage(new ThemesPage());
                    break;
                case "History":
                    Detail = new NavigationPage(new HistoryPage());
                    break;
                case "Exercises":
                    Detail = new NavigationPage(new ExercisePage());
                    break;
            }
            currItem = item;
        }
    }
}