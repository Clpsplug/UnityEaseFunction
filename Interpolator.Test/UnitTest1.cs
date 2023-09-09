using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace ExplodingCable.Interpolators.Test
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
            Interpolator linear = new LinearInterpolator(0, 100, true, true);
            Interpolator easeIn = new EaseInInterpolator(0, 100, false, 2);
            Interpolator easeOut = new EaseOutInterpolator(0, 100, false, 2);
            AreEqual(50.00000, linear.Get(0.5, 1), "0-100 linear 0.5 = 50");
            AreEqual(25.00000, easeIn.Get(0.5, 1), "0-100 EaseIn Pow 2 0.5 = 25");
            AreEqual(70.71067, easeOut.Get(0.5, 1), "0-100 EaseOut Pow 2 0.5 = 70.71067");
        }

        [Test]
        public void CanCalculateFromDirectRate()
        {
            Interpolator linear = new LinearInterpolator(0, 100, true, true);
            AreEqual(50,linear.Get(0.5), "0-100 linear 0.5 = 50");
        }
        
        [Test]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void ConformsToBoundaries()
        {
            Interpolator linearTT = new LinearInterpolator(0, 100, true, true);
            Interpolator linearTF = new LinearInterpolator(0, 100, true, false);
            Interpolator linearFT = new LinearInterpolator(0, 100, false, true);
            Interpolator linearFF = new LinearInterpolator(0, 100, false, false);

            AreEqual(-100, linearTT.Get(-1, 1), "Undershoots");
            AreEqual(200, linearTT.Get(2, 1), "Overshoots");
            AreEqual(0, linearTF.Get(-1, 1), "Does not undershoot");
            AreEqual(200, linearTF.Get(2, 1), "Overshoots");
            AreEqual(-100, linearFT.Get(-1, 1), "Undershoots");
            AreEqual(100, linearFT.Get(2, 1), "Does not Overshoot");
            AreEqual(0, linearFF.Get(-1, 1), "Does not undershoot");
            AreEqual(100, linearFF.Get(2, 1), "Does not Overshoot");
        }

        [Test]
        public void CanPerformDirectInterpolation()
        { 
            var interpolator = new Interpolator(0, 100);
            AreEqual(50, interpolator.Interpolate(0.5), "0-100 Direct interpolation 0.5 = 50");
        }
        
        private void AreEqual(double expected, double actual, string mes)
        {
            Assert.LessOrEqual(Math.Abs(expected - actual), Epsilon, mes);
        }
    }
}