using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using ExplodingCable.Interpolator;

namespace Interpolator.Test
{
    public class Tests
    {
        private const double Epsilon = 0.0001;

        [SetUp]
        public void Setup()
        { }

        [Test]
        public void CanCalculateFromCurrentAndMax()
        {
            InterpolatorBase linear = new LinearInterpolator(0, 100, true, true);
            InterpolatorBase easeIn = new EaseInInterpolator(0, 100, false, 2);
            InterpolatorBase easeOut = new EaseOutInterpolator(0, 100, false, 2);
            AreEqual(50.00000, linear.Get(0.5, 1), "0-100 linear 0.5 = 50");
            AreEqual(25.00000, easeIn.Get(0.5, 1), "0-100 EaseIn Pow 2 0.5 = 25");
            AreEqual(70.71067, easeOut.Get(0.5, 1), "0-100 EaseOut Pow 2 0.5 = 70.71067");
        }

        [Test]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void ConformsToBoundaries()
        {
            InterpolatorBase linearTF = new LinearInterpolator(0, 100, true, false);
            InterpolatorBase linearFT = new LinearInterpolator(0, 100, false, true);
            InterpolatorBase linearFF = new LinearInterpolator(0, 100, false, false);

            AreEqual(0, linearTF.Get(-1, 1), "Does not undershoot");
            AreEqual(200, linearTF.Get(2, 1), "Overshoots");
            AreEqual(-100, linearFT.Get(-1, 1), "Undershoots");
            AreEqual(100, linearFT.Get(2, 1), "Does not Overshoot");
            AreEqual(0, linearFF.Get(-1, 1), "Does not undershoot");
            AreEqual(100, linearFF.Get(2, 1), "Does not Overshoot");
        }

        private void AreEqual(double expected, double actual, string mes)
        {
            Assert.LessOrEqual(Math.Abs(expected - actual), Epsilon, mes);
        }
    }
}