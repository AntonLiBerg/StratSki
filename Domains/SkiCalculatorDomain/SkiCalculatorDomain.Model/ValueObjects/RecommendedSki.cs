using System;
using System.Collections.Generic;
using System.Text;

namespace SkiCalculatorDomain.Model.ValueObjects
{
    public class RecommendedSki
    {
        public enum SkiLengthUnits
        {
            cm = 1,
        }
        public IRecommendedSkiLength LengthRecommendation { get; protected set; }
        public string Unit { get; protected set; }
        public RecommendedSki(double length, SkiLengthUnits unit = SkiLengthUnits.cm)
        {
            LengthRecommendation = new ExactLengthSkiRecommendation(length);
            Unit = unit.ToString();
        }
        public RecommendedSki(double fromLength, double toLength, SkiLengthUnits unit = SkiLengthUnits.cm)
        {
            LengthRecommendation = new SpanLengthSkiRecommendation(fromLength,toLength);
            Unit = unit.ToString();
        }

        public interface IRecommendedSkiLength{}
        public class ExactLengthSkiRecommendation : IRecommendedSkiLength
        {
            public double ExactLength { get; set; }

            public ExactLengthSkiRecommendation(double length)
            {
                ExactLength = length;
            }
        }
        public class SpanLengthSkiRecommendation : IRecommendedSkiLength
        {
            public double LengthFrom { get; set; }
            public double LengthTo { get; set; }
            public SpanLengthSkiRecommendation(double lengthFrom, double lengthTo)
            {
                LengthFrom = lengthFrom;
                LengthTo = lengthTo;
            }

        }

    }
}
