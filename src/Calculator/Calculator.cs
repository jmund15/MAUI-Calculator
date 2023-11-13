using System.Diagnostics;

namespace Calculator;

public static class Calculator
{
    public static double Calculate(double value1, double value2, string mathOperator)
    {
        double result = 0;

        switch (mathOperator)
        {
            case "÷":
                result = value1 / value2;
                break;
            case "×":
                result = value1 * value2;
                break;
            case "+":
                result = value1 + value2;
                break;
            case "-":
                result = value1 - value2;
                break;
            case "^":
                result = Math.Pow(value1, value2);
                break;
            case "mod":
                result = value1 % value2;
                break;
        }

        return result;
    }

    public static double NewCalculate(List<string> equation)
    {
        double result = 0;

        double value1 = Double.Parse(equation[0]);
        string op = "";
        double value2 = 0;

        for (int i = 0; i < equation.Count; i ++)
        {
            Trace.Write(equation[i] + ", ");
        }
        Trace.Write("\n");
        for (int i = 1; i < equation.Count; i += 2)
        {
            op = equation[i];
            value2 = Double.Parse(equation[i + 1]);

            switch (op)
            {
                case "÷":
                    value1 /= value2;
                    break;
                case "×":
                    value1 *= value2;
                    break;
                case "+":
                    value1 += value2;
                    break;
                case "-":
                    value1 -= value2;
                    break;
                case "^":
                    value1 = Math.Pow(value1, value2);
                    break;
                case "mod":
                    value1 %= value2;
                    break;
            }
        }
        result = value1;

        return result;
    }
}
public static class DoubleExtensions
{
    public static string ToTrimmedString(this double target, string decimalFormat)
    {
        string strValue = target.ToString(decimalFormat); //Get the stock string

        //If there is a decimal point present
        if (strValue.Contains("."))
        {
            //Remove all trailing zeros
            strValue = strValue.TrimEnd('0');

            //If all we are left with is a decimal point
            if (strValue.EndsWith(".")) //then remove it
                strValue = strValue.TrimEnd('.');
        }

        return strValue;
    }
}
