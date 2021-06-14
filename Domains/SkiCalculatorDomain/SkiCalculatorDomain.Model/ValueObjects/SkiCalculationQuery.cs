using SkiCalculatorDomain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SkiCalculatorDomain.Model.ValueObjects
{
    public sealed class SkiCalculationQuery : IEquatable<SkiCalculationQuery>
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

        public bool Equals([AllowNull] SkiCalculationQuery other)
        {
            if (other is null)
                return false;
            return other.GetHashCode() == this.GetHashCode();
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Age.GetHashCode();
                hash = hash * 23 + Height.GetHashCode();
                hash = hash * 23 + Style.GetHashCode();
                return hash;
            }
        }
    }
}
