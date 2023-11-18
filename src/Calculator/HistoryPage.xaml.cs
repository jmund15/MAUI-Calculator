namespace Calculator;

public partial class HistoryPage : ContentPage
{
	public HistoryPage()
	{
		InitializeComponent();
        //BindingContext = new EquationViewModel();
        SetBinding();
    }

	private async Task SetBinding()
	{
        await App.EquationViewModel.UpdateEquations();
        BindingContext = App.EquationViewModel;
    }

    public async void OnClear(object sender, EventArgs e)
    {
        await App.EquationViewModel.ClearEquationAsync();
    }


}