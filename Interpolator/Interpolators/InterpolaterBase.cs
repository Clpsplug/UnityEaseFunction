using System;

namespace ExplodingCable.Interpolators
{
    public class Interpolator
    {
        private readonly double _start;
        private readonly double _end;


        /// <summary>
        /// Base constructor for interpolators
        /// </summary>
        /// <param name="start">Value at the start of interpolation.</param>
        /// <param name="end">Value at the end of interpolation.</param>
        public Interpolator(double start, double end)
        {
            _start = start;
            _end = end;
        }

        /// <summary>
        /// Get the interpolated value.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public double Get(double current, double max)
        {
            if (max == 0)
            {
                throw new DivideByZeroException("Max cannot be zero.");
            }
            return Get(current / max);
        }

        public virtual double Get(double rate)
        {
            return Interpolate(rate);
        }
        
        public double Interpolate(double rate)
        {
            return _start + (_end - _start) * rate;
        }
    }
}