using CommandDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBus.App.Functions
{
    public class ReturnResultDefaultFunction : IFunction
    {
        public decimal Number1 { get; set; }
        public decimal Number2 { get; set; }

        public ReturnResultDefaultFunction(decimal number1, decimal number2)
        {
            Number1 = number1;
            Number2 = number2;
        }
    }
}