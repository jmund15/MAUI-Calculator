using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SQLite;

namespace Calculator.Model
{
    [Table("equation")]
    public class Equation
    {
        [PrimaryKey]
        public int Id { get; set; }
        [MaxLength(100), Unique]
        public string Expression { get; set; }
        [MaxLength(25), Unique]
        public string Answer { get; set; }
        //public Equation(int eqNum, string expression, string answer)
        //{
        //    Id = eqNum;
        //    Expression = expression;
        //    Answer = answer;
        //}
    }
}
