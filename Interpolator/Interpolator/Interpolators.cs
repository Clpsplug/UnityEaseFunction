using System;

namespace ExplodingCable.Interpolator
{
    public class LinearInterpolator : InterpolatorBase
    {
        public LinearInterpolator(double start, double end, bool canOvershoot, bool canUndershoot)
            : base(start, end, canOvershoot, canUndershoot)
        { }

        public override double Get(double current, double max)
        {
            var rate = current / max;
            if (!CanOvershoot)
            {
                rate = Math.Min(rate, 1.0);
            }

            if (!CanUndershoot)
            {
                rate = Math.Max(rate, 0.0);
            }

            return Interpolate(rate);
        }
    }

    public class EaseInInterpolator : InterpolatorBase
    {
        private readonly double _power;

        public EaseInInterpolator(double start, double end, bool canOvershoot, double power)
            : base(start, end, canOvershoot, false)
        {
            if (power < 0.0)
            {
                throw new ArgumentException($"Invalid parameter, {nameof(power)} < 0.0.");
            }
            _power = power;
        }

        public override double Get(double current, double max)
        {
            var rate = Math.Pow(Math.Abs(current / max), _power);
            if (!CanOvershoot)
            {
                rate = Math.Min(rate, 1.0);
            }

            return Interpolate(rate);
        }
    }

    public class EaseOutInterpolator : InterpolatorBase
    {
        private readonly double _power;

        public EaseOutInterpolator(double start, double end, bool canOvershoot, double power)
            : base(start, end, canOvershoot, false)
        {
            if (power <= 0.0)
            {
                throw new ArgumentException($"Invalid parameter, {nameof(power)} <= 0.0.");
            }

            _power = power;
        }

        public override double Get(double current, double max)
        {
            var rate = Math.Pow(Math.Abs(current / max), 1 / _power);

            if (!CanOvershoot)
            {
                rate = Math.Min(rate, 1.0);
            }

            return Interpolate(rate);
        }
    }
}