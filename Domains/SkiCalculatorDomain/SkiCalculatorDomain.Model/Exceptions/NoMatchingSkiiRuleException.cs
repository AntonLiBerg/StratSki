using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Model.Exceptions
{
    public class NoMatchingSkiRuleException : Exception
    {
        public NoMatchingSkiRuleException()
        {
        }

        public NoMatchingSkiRuleException(string message)
            : base(message)
        {
        }

        public NoMatchingSkiRuleException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
