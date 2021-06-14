using SkiCalculatorDomain.Model.Enums;
using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules
{
    public class FreeStyleRule : IRecommendedSkiRuleChain
    {
        public override RecommendedSki Calculate(SkiCalculationQuery query)
        {
            if (query.Style == SkiStyleEnum.FreeStyle)
                return new RecommendedSki(query.Height + 10, query.Height + 15);
            return NextChain.Calculate(query);
        }
    }
}
