using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using static SkiCalculatorDomain.Model.ValueObjects.RecommendedSki;

namespace SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules
{
    public class ChildRule : IRecommendedSkiRuleChain
    {
        public override RecommendedSki Calculate(SkiCalculationQuery query)
        {
            if (query.Age >= 5 && query.Age <= 8)
                return new RecommendedSki(query.Height+10, query.Height+20);
            return NextChain.Calculate(query);
        }
    }
}
