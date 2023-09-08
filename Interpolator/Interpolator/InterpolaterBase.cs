namespace ExplodingCable.Interpolator
{
    public abstract class InterpolatorBase
    {
        private readonly double _start;
        private readonly double _end;

        protected readonly bool CanOvershoot;
        protected readonly bool CanUndershoot;

        protected InterpolatorBase(double start, double end, bool canOvershoot, bool canUndershoot)
        {
            _start = start;
            _end = end;
            CanOvershoot = canOvershoot;
            CanUndershoot = canUndershoot;
        }

        public abstract double Get(double current, double max);

        public double Interpolate(double rate)
        {
            return _start + (_end - _start) * rate;
        }
    }
}