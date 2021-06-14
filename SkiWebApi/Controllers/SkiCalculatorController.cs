using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkiCalculatorDomain.Service.DomainLayer;
using SkiCalculatorDomain.Model.Enums;
using SkiCalculatorDomain.Model.Exceptions;
using SkiCalculatorDomain.Service.ServiceLayer;
using SkiCalculatorDomain.Model.ValueObjects;
using static SkiCalculatorDomain.Model.ValueObjects.RecommendedSki;
using SkiWebApi.Responses;
using LoggingDomain.Service.ServiceLayer;
using LoggingDomain.Model.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkiiWebApi.Controllers
{
    [ApiController]
    public class SkiCalculatorController : ControllerBase
    {
        public ISkiCalculatorService SkiCalculatorService;
        public ILoggingService LoggingService;
        public SkiCalculatorController(ISkiCalculatorService skiCalculatorService,ILoggingService loggingService)
        {
            SkiCalculatorService = skiCalculatorService;
            LoggingService = loggingService;
        }
        public class SkiStylesResponse: IResponse
        {
            public List<string> Styles { get; set; }
            public SkiStylesResponse(List<string> styles)
            {
                Styles = styles;
            }
        }
        [HttpGet()]
        [Route("api/SkiCalculator/getskistyles")]
        public IActionResult GetSkiStyles()
        {
            try
            {
                LoggingService.Log("SkiCalculatorController", "GetSkiStyles", LogSeverity.Info);
                IList<SkiStyleEnum> styles = SkiCalculatorService.GetSkiStyles();
                var res = styles.Select(s => s.ToString()).ToList();
                return Ok(new SkiStylesResponse(res));
            }
            catch (Exception e)
            {
                LoggingService.Log("SkiCalculatorController", $"GetSkiStyles Exception: {e.Message}",LogSeverity.Error);
                return StatusCode(500);
            }
        }
        public class ExactLengthSkiRecommendationResponse : IResponse
        {
            public double ExactLength { get; set; }
            public string Unit { get; set; }
            public ExactLengthSkiRecommendationResponse(RecommendedSki recommendedSkii)
            {
                ExactLength = ((RecommendedSki.ExactLengthSkiRecommendation)recommendedSkii.LengthRecommendation).ExactLength;
                Unit = recommendedSkii.Unit.ToString();
            }
        }
        public class LengthSpanSkiRecommendationResponse : IResponse
        {
            public double LengthFrom { get; set; }
            public double LengthTo { get; set; }
            public string Unit { get; set; }
            public LengthSpanSkiRecommendationResponse(RecommendedSki recommendedSkii)
            {
                LengthFrom = ((SpanLengthSkiRecommendation)recommendedSkii.LengthRecommendation).LengthFrom;
                LengthTo = ((SpanLengthSkiRecommendation)recommendedSkii.LengthRecommendation).LengthTo;
                Unit = recommendedSkii.Unit.ToString();
            }
        }
        public class NoLengthRecommendationResponse : IResponse
        {

            public static string NoRecommendationMessage = "Unfortunately, we dont have a recommendation for you";
            public String NoRecommendation { get; set; }
            public NoLengthRecommendationResponse()
            {
                NoRecommendation = NoRecommendationMessage;
            }
        }
        [HttpGet()]
        [Route("api/SkiCalculator/getskirecommendation")]
        public IActionResult GetSkiRecommendation([FromQuery] int age, [FromQuery] int height, [FromQuery] string style)
        {
            try
            {
                LoggingService.Log("SkiCalculatorController", $"GetSkiRecommendation: age: {age}, height:{height}, style:{style}", LogSeverity.Info);
                SkiCalculationQuery scQuery = new SkiCalculationQuery(age,height,style);
                RecommendedSki recommendation = SkiCalculatorService.GetSkiLengthRecommendation(scQuery);

                if (recommendation.LengthRecommendation is RecommendedSki.ExactLengthSkiRecommendation)
                    return Ok(new ExactLengthSkiRecommendationResponse(recommendation));
                else
                    return Ok(new LengthSpanSkiRecommendationResponse(recommendation));
            }
            catch (Exception e)
            {
                LoggingService.Log("SkiCalculatorController", $"GetSkiRecommendation Exception: {e.Message}", LogSeverity.Error);
                if (e is NoMatchingSkiRuleException)
                    return Ok(new NoLengthRecommendationResponse());                
                return StatusCode(500);
            }
        }
    }
}
