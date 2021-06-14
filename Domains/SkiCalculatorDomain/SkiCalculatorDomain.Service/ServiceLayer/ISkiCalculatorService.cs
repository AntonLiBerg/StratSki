using SkiCalculatorDomain.Model.Enums;
using SkiCalculatorDomain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Service.ServiceLayer
{
    public interface ISkiCalculatorService
    {
        RecommendedSki GetSkiLengthRecommendation(SkiCalculationQuery scQuery);
        IList<SkiStyleEnum> GetSkiStyles();
    }
}
