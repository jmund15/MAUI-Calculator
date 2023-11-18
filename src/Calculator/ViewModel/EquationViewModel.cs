
namespace Calculator.ViewModel
{
    public partial class EquationViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        public ObservableCollection<Equation> Equations { get; } = new();
        public EquationViewModel()
        {
            GetEquationsAsync();
        }

        [RelayCommand]
        async Task GetEquationsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var equations = new List<Equation> { new Equation(1, "1+1", "2"), new Equation(2, "1-1", "0") }; //sqlite pull here

                if (Equations.Count != 0)
                    Equations.Clear();

                foreach (var equation in equations)
                {
                    Equations.Add(equation);
                }
                Trace.WriteLine("Sucessfully got equations");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Unable to get equations: {ex.Message}");
                //await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }   

        async Task ClearEquationAsync()
        {

        }
    }
}
