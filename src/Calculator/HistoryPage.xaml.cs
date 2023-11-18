namespace Calculator;

public partial class HistoryPage : ContentPage
{
	public FormattedString equationString = new FormattedString();
	public HistoryPage()
	{
		InitializeComponent();
		BindingContext = new EquationViewModel();

		// equationString.Spans.Add()
	}
}