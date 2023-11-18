namespace Calculator;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();
        OnClear(this, null);
    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    List<string> fullEquation = new List<string>();
    string decimalFormat = "N0";

    void OnSelectNumber(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;

        if (currentState == -2)
        {
            fullEquation.Add(currentEntry);
            currentEntry = string.Empty;
        }

        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            this.resultText.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }

        this.resultText.Text += pressed;

        if (fullEquation.Count != 0)
        {
            fullEquation.RemoveAt(fullEquation.Count - 1);
        }
        fullEquation.Add(currentEntry);
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        if (currentState != -1)
        {
            currentEntry = string.Empty;
            //fullEquation.Add(currentEntry);
            //CurrentCalculation.Text = string.Join("", fullEquation);
            //currentEntry = string.Empty;
        }
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
        }
        else if (currentState == -2)
        {
            fullEquation.RemoveAt(fullEquation.Count - 1);
        }
        else
        {
            OnCalculate(this, null);
        }

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        mathOperator = pressed;
        fullEquation.Add(pressed);
        CurrentCalculation.Text = string.Join("", fullEquation);
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N2";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
        fullEquation.Clear();
        this.CurrentCalculation.Text = string.Empty;
    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            CurrentCalculation.Text = string.Join("", fullEquation);

            if (secondNumber == 0)
            {
                LockNumberValue(resultText.Text);
            }

            //double result = Calculator.Calculate(firstNumber, secondNumber, mathOperator);
            double result = Calculator.NewCalculate(fullEquation);

            //this.CurrentCalculation.Text = $"{firstNumber} {mathOperator} {secondNumber}";

            this.resultText.Text = result.ToTrimmedString(decimalFormat);
            firstNumber = result;
            secondNumber = 0;
            currentState = -1;
            currentEntry = string.Empty;

            App.EquationViewModel.AddNewEquation(CurrentCalculation.Text, resultText.Text);
        }
    }

    void OnNegative(object sender, EventArgs e)
    {
        if (currentState == 1 || currentState == -1)
        {
            secondNumber = -1;
            mathOperator = "×";
            currentState = 2;
            fullEquation.Add(mathOperator);
            fullEquation.Add(secondNumber.ToString());
            CurrentCalculation.Text = string.Join("", fullEquation);
            OnCalculate(this, null);
        }
    }
    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1 || currentState == -1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "×";
            currentState = 2;
            fullEquation.Add(mathOperator);
            fullEquation.Add(secondNumber.ToString());
            CurrentCalculation.Text = string.Join("", fullEquation);
            OnCalculate(this, null);
        }
    }
}
