using CommandDispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBus.App.Functions
{
    public class ConcateNameFunction : IFunction<String>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ConcateNameFunction(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
