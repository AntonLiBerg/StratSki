using SkiCalculatorDomain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SkiCalculatorDomain.Model.ValueObjects
{
    public sealed class SkiCalculationQuery
    {
        public int Age { get; set; }
        public int Height { get; set; }
        public SkiStyleEnum Style { get; set; }
        public SkiCalculationQuery(int age, int height, SkiStyleEnum style)
        {
            Age = age;
            Height = height;
            Style = style;
        }

        public SkiCalculationQuery(int age, int height, string style)
        {
            Age = age;
            Height = height;
            SkiStyleEnum varName = (SkiStyleEnum)Enum.Parse(typeof(SkiStyleEnum), style, true);
            Style = varName;
        }
    }
}
