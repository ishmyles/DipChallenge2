using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinnyVetUI.Classes
{
    public class ValidationFailureException : Exception
    {
        public ValidationFailureException() : base() { }

        public ValidationFailureException(string input) : base("ValidationFailureException: " + input) { }
    }
}
