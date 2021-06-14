using SkiCalculatorDomain.Model.Enums;
using SkiCalculatorDomain.Model.Factories;
using SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules;
using SkiCalculatorDomain.Service.ServiceLayer;
using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiCalculatorDomain.Service.DomainLayer
{
    public class SkiCalculatorService : ISkiCalculatorService
    {
        protected IRecommendedSkiRuleChain skiRuleChain;
        public SkiCalculatorService()
        {
            skiRuleChain = SkiiRuleChainFactory.MakeStandardChain();
        }
        public SkiCalculatorService(IRecommendedSkiRuleChain ruleChain)
        {
            skiRuleChain = ruleChain;
        }

        public IList<SkiStyleEnum> GetSkiStyles() 
            => Enum.GetValues(typeof(SkiStyleEnum)).OfType<SkiStyleEnum>().ToList();

        public RecommendedSki GetSkiLengthRecommendation(SkiCalculationQuery query) 
            => skiRuleChain.Calculate(query);
    }
}
