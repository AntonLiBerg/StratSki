using SkiCalculatorDomain.Model.Exceptions;
using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules
{
    public class DefaultCaseRule : IRecommendedSkiRuleChain
    {
        public DefaultCaseRule()
        {

        }
        public override RecommendedSki Calculate(SkiCalculationQuery query)
        {
            var message = $"No matching rule found for query! Age: {query.Age}, Height: {query.Height}, Style: {query.Style}";
            throw new NoMatchingSkiRuleException(message);
        }

        public new void SetNextChain(IRecommendedSkiRuleChain nextChain) 
            => throw new NotImplementedException();
    }
}
