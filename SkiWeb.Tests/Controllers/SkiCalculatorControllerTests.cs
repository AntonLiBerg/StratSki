using LoggingDomain.Service.ServiceLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SkiCalculatorDomain.Model.Enums;
using SkiCalculatorDomain.Model.Exceptions;
using SkiCalculatorDomain.Model.ValueObjects;
using SkiCalculatorDomain.Service.ServiceLayer;
using SkiiWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static SkiiWebApi.Controllers.SkiCalculatorController;

namespace SkiWeb.Tests.Controllers
{

    [TestClass]
    public class SkiCalculatorControllerTests
    {
        private Mock<ISkiCalculatorService> SkiCalculatorService;
        private Mock<ILoggingService> LoggingService;

        [TestInitialize]
        public void InitTest()
        {
            SkiCalculatorService = new Mock<ISkiCalculatorService>();
            LoggingService = new Mock<ILoggingService>();
        }

        [TestMethod]
        public void GetSkiRecommendation_NoRecommendation_NoLengthRecommendationResponse()
        {
            //Arrange
            SkiCalculatorService.Setup(sc =>
                sc.GetSkiLengthRecommendation(It.IsAny<SkiCalculationQuery>()))
                .Returns((SkiCalculationQuery q) => throw new NoMatchingSkiRuleException());
            SkiCalculatorController ctrl = new SkiCalculatorController(SkiCalculatorService.Object, LoggingService.Object);
            //Act
            OkObjectResult res = (OkObjectResult) ctrl.GetSkiRecommendation(0, 0, SkiStyleEnum.None.ToString());
            //Assert
            Assert.AreEqual(200,res.StatusCode);
            Assert.IsTrue(res.Value is NoLengthRecommendationResponse);
        }
        [TestMethod]
        public void GetSkiRecommendation_ExactLengthRecommendation_ExactLengthSkiRecommendationResponse()
        {
            //Arrange
            int expectedExactLength = 1;
            SkiCalculatorService.Setup(sc =>
                sc.GetSkiLengthRecommendation(It.IsAny<SkiCalculationQuery>()))
                .Returns((SkiCalculationQuery q) => new RecommendedSki(expectedExactLength));
            SkiCalculatorController ctrl = new SkiCalculatorController(SkiCalculatorService.Object, LoggingService.Object);
            //Act
            OkObjectResult res = (OkObjectResult)ctrl.GetSkiRecommendation(0, 0, SkiStyleEnum.None.ToString());
            //Assert
            Assert.AreEqual(200, res.StatusCode);
            Assert.AreEqual(expectedExactLength, ((ExactLengthSkiRecommendationResponse)res.Value).ExactLength);
        }
        [TestMethod]
        public void GetSkiRecommendation_LengthSpanRecommendation_LengthSpanSkiRecommendationResponse()
        {
            //Arrange
            int LengthFrom = 1;
            int LengthTo = 2;
            SkiCalculatorService.Setup(sc =>
                sc.GetSkiLengthRecommendation(It.IsAny<SkiCalculationQuery>()))
                .Returns((SkiCalculationQuery q) => new RecommendedSki(LengthFrom,LengthTo));
            SkiCalculatorController ctrl = new SkiCalculatorController(SkiCalculatorService.Object, LoggingService.Object);
            //Act
            OkObjectResult res = (OkObjectResult)ctrl.GetSkiRecommendation(0, 0, SkiStyleEnum.None.ToString());
            //Assert
            LengthSpanSkiRecommendationResponse nRes = ((LengthSpanSkiRecommendationResponse)res.Value);
            Assert.AreEqual(200, res.StatusCode);
            Assert.AreEqual(LengthFrom, nRes.LengthFrom);
            Assert.AreEqual(LengthTo, nRes.LengthTo);
        }
        [TestMethod]
        public void GetSkiStyles_SkiStylesResponse()
        {
            //Arrange
            SkiCalculatorService.Setup(s => s.GetSkiStyles())
                .Returns(() => Enum.GetValues(typeof(SkiStyleEnum)).OfType<SkiStyleEnum>().ToList());
            SkiCalculatorController ctrl = new SkiCalculatorController(SkiCalculatorService.Object, LoggingService.Object);
            //Act
            SkiStylesResponse res = (SkiStylesResponse)((OkObjectResult)ctrl.GetSkiStyles()).Value;
            //Assert
            IList<string> expected = Enum.GetValues(typeof(SkiStyleEnum)).OfType<SkiStyleEnum>().Select(s => s.ToString()).ToList();
            Assert.IsTrue(expected.All(res.Styles.Contains));
        }
    }
}
