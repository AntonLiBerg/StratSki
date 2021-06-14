using SkiCalculatorDomain.Model.Enums;
using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules
{
    public class ClassicStyleRule : IRecommendedSkiRuleChain
    {
        public override RecommendedSki Calculate(SkiCalculationQuery query)
        {
            if (query.Style == SkiStyleEnum.Classic)
                return new RecommendedSki(Math.Min(query.Height + 20,207));
            return NextChain.Calculate(query);
        }
    }
}
