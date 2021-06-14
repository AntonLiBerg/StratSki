using SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Model.Factories
{
    public static class SkiiRuleChainFactory
    {
        public static IRecommendedSkiRuleChain MakeStandardChain()
        {
            SmallChildRule startRule = new SmallChildRule();

            startRule.SetNextChain(new ChildRule())
                .SetNextChain(new ChildRule())
                .SetNextChain(new ClassicStyleRule())
                .SetNextChain(new FreeStyleRule());
            return startRule;
        }
    }
}
