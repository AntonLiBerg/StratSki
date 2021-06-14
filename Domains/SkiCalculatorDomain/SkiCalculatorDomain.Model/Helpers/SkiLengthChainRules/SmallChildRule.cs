using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using static SkiCalculatorDomain.Model.ValueObjects.RecommendedSki;

namespace SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules
{
    public class SmallChildRule : IRecommendedSkiRuleChain
    {
        public override RecommendedSki Calculate(SkiCalculationQuery query)
        {
            if (query.Age >= 0 && query.Age <= 4)
                return new RecommendedSki(query.Height);
            return NextChain.Calculate(query);
        }
    }
}
