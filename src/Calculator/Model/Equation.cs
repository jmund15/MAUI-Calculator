using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Calculator.Model
{
    public class Equation
    {
        public Equation(int eqNum, string expression, string answer)
        {
            EquationNum = eqNum;
            Expression = expression;
            Answer = answer;
        }

        public string Expression { get; set; }
        public string Answer { get; set; }
        public int EquationNum { get; set; }
    }
}
