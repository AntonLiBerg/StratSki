using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkiCalculatorDomain.Model.Enums;
using SkiCalculatorDomain.Model.Exceptions;
using SkiCalculatorDomain.Model.Helpers.SkiiLengthChainRules;
using SkiCalculatorDomain.Model.ValueObjects;
using SkiCalculatorDomain.Service.DomainLayer;
using System;
using System.Collections.Generic;
using System.Text;
using static SkiCalculatorDomain.Model.ValueObjects.RecommendedSki;

namespace SkiWeb.Tests.Domains.SkiCalculatorDomainTests
{
    [TestClass]
    public class SkiCalculatorServiceTests
    {
        private class TestSkiChainRule : IRecommendedSkiRuleChain
        {
            public override RecommendedSki Calculate(SkiCalculationQuery query)
            {
                return new RecommendedSki(0, SkiLengthUnits.cm);
            }
        }
        private class TestGotoNextSkiChainRule : IRecommendedSkiRuleChain
        {
            public override RecommendedSki Calculate(SkiCalculationQuery query)
            {
                return NextChain.Calculate(query);
            }
        }
        [TestMethod]
        public void GetSkiiLengthRecommendation_DefaultCase_errorThrown()
        {
            try
            {
                //Arrange
                IRecommendedSkiRuleChain rulesChain = new DefaultCaseRule();
                SkiCalculatorService skiService = new SkiCalculatorService(rulesChain);
                SkiCalculationQuery query = new SkiCalculationQuery(0, 0, SkiStyleEnum.None);
                //Act
                var res = skiService.GetSkiLengthRecommendation(query);
                //Assert
                Assert.Fail();
            }
            catch (NoMatchingSkiRuleException) {
                Assert.IsTrue(true);
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetSkiiLengthRecommendation_TestSkiChainRule_NoException()
        {
            try
            {
                //Arrange
                IRecommendedSkiRuleChain rulesChain = new TestGotoNextSkiChainRule()
                    .SetNextChain(new TestSkiChainRule());
                SkiCalculatorService skiService = new SkiCalculatorService(rulesChain);
                SkiCalculationQuery query = new SkiCalculationQuery(0, 0, SkiStyleEnum.None);
                //Act
                var res = skiService.GetSkiLengthRecommendation(query);
                //Assert
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        public void GetSkiiLengthRecommendation_TestGotoNextSkiChainRule_NoException()
        {
            try
            {
                //Arrange
                IRecommendedSkiRuleChain rulesChain = new TestGotoNextSkiChainRule()
                    .SetNextChain(new TestSkiChainRule());
                SkiCalculatorService skiService = new SkiCalculatorService(rulesChain);
                SkiCalculationQuery query = new SkiCalculationQuery(0, 0, SkiStyleEnum.None);
                //Act
                var res = skiService.GetSkiLengthRecommendation(query);
                //Assert
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
