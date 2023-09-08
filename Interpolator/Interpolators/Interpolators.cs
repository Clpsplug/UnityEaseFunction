using System;

namespace ExplodingCable.Interpolators
{
    public class LinearInterpolator : Interpolator
    {
        private readonly bool _canOvershoot;
        private readonly bool _canUndershoot;

        /// <summary>
        /// Linear Interpolator
        /// </summary>
        /// <param name="start">Value at the start of interpolation.</param>
        /// <param name="end">Value at the end of interpolation.</param>
        /// <param name="canOvershoot">If true, value can go past end value on over 100% progression.</param>
        /// <param name="canUndershoot">If true, value can go past start value on negative progression.</param>
        public LinearInterpolator(double start, double end, bool canOvershoot, bool canUndershoot)
            : base(start, end)
        {
            _canOvershoot = canOvershoot;
            _canUndershoot = canUndershoot;
        }

        public override double Get(double rate)
        {
            var linearRate = rate;
            if (!_canOvershoot)
            {
                linearRate = Math.Min(linearRate, 1.0);
            }

            if (!_canUndershoot)
            {
                linearRate = Math.Max(linearRate, 0.0);
            }

            return Interpolate(linearRate);
        }
    }

    public class EaseInInterpolator : Interpolator
    {
        private readonly bool _canOvershoot;
        private readonly double _power;
        
        public EaseInInterpolator(double start, double end, bool canOvershoot, double power)
            : base(start, end)
        {
            if (power < 0.0)
            {
                throw new ArgumentException($"Invalid parameter, {nameof(power)} < 0.0.");
            }

            _canOvershoot = canOvershoot;
            _power = power;
        }

        public override double Get(double rate)
        {
            var easeInRate = Math.Pow(Math.Abs(rate), _power);
            if (!_canOvershoot)
            {
                easeInRate = Math.Min(easeInRate, 1.0);
            }

            return Interpolate(easeInRate);
        }
    }

    public class EaseOutInterpolator : Interpolator
    {
        private readonly bool _canOvershoot;
        private readonly double _power;

        public EaseOutInterpolator(double start, double end, bool canOvershoot, double power)
            : base(start, end)
        {
            if (power <= 0.0)
            {
                throw new ArgumentException($"Invalid parameter, {nameof(power)} <= 0.0.");
            }

            _canOvershoot = canOvershoot;
            _power = power;
        }

        public override double Get(double rate)
        {
            var easeOutRate = Math.Pow(Math.Abs(rate), 1 / _power);

            if (!_canOvershoot)
            {
                easeOutRate = Math.Min(easeOutRate, 1.0);
            }

            return Interpolate(easeOutRate);
        }
    }
}