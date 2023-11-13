namespace Calculator;

using Resources;
using System.Diagnostics;

public partial class ThemesPage : ContentPage
{
    public ThemesPage()
	{
		InitializeComponent();
	}
    private void ChangeTheme(object sender, EventArgs e)
    {
        var button = sender as Button;
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null)
        {
            mergedDictionaries.Clear();
            if (sender == this.PinkTheme)
            {
                mergedDictionaries.Add(new PinkTheme()); 
            }
            else if (sender == this.RedTheme) {
                mergedDictionaries.Add(new RedTheme());
            }
            else if (sender == this.LightTheme)
            {
                mergedDictionaries.Add(new LightTheme());
            }
            else if (sender == this.DarkTheme)
            {
                mergedDictionaries.Add(new DarkTheme());
            }
            else
            {
                Trace.WriteLine(button.Text + "is the name of the theme button clicked. No theme available");
            }
            
        }
    }
}