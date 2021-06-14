using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules
{
    public abstract class IRecommendedSkiRuleChain
    {
        private IRecommendedSkiRuleChain _nextChain;
        protected IRecommendedSkiRuleChain NextChain 
        {
            get 
            {
                if (_nextChain == null)
                    _nextChain = new DefaultCaseRule();
                return _nextChain;
            }
            private set { _nextChain = value; }
        }
        public abstract RecommendedSki Calculate(SkiCalculationQuery query);
        public IRecommendedSkiRuleChain SetNextChain(IRecommendedSkiRuleChain nextChain)
        {
            _nextChain = nextChain;
            return nextChain;
        }
    }
}
