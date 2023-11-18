namespace Calculator;

public partial class App : Application
{
    public static EquationViewModel EquationViewModel { get; private set; }

    public App(EquationViewModel eqViewModel)
	{
		InitializeComponent();

		MainPage = new FlyoutMainPage();

		EquationViewModel = eqViewModel;
    }
}
