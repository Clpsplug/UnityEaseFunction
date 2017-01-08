using System;
using System.Collections;

public static class EaseFunctions {

	/// <summary>
	/// Linear Ease Functions
	/// </summary>
	/// <param name="start">Beginning Value</param>
	/// <param name="end">Ending Value</param>
	/// <param name="current">Current Progress (against Max)</param>
	/// <param name="max">Maximum Progress</param>
	/// <param name="overshoot">True to allow the return to exceed ending value, when current > max</param>
	/// <param name="undershoot">True to allow the return to fall behind 0, when current < 0</param>
	/// <returns>Value between start and end, based on the progression</returns>
	public static float Linear(double start, double end, double current, double max, bool overshoot, bool undershoot)
	{

		float rate = (float)(current / max);
		if (!overshoot) {
			rate = rate > 1.0f ? 1.0f : rate;
		}
		if (!undershoot) {
			rate = rate < 0.0f ? 0.0f : rate;
		}
		return (float)(start + (end-start) * rate);

	}

	/// <summary>
	/// Monomial Shaping Ease Function (Accelerated)
	/// </summary>
	/// <param name="start">Beginning Value</param>
	/// <param name="end">Ending Value</param>
	/// <param name="power">Progression will be raised by this.</param>
	/// <param name="current">Current Progress (against Max)</param>
	/// <param name="max">Maximum Progress</param>
	/// <param name="overshoot">True to allow the return to exceed ending value, when current > max</param>
	/// <param name="undershoot">True to allow the return to fall behind 0, when current < 0</param>
	/// <returns>Value between start and end, based on the progression</returns>
	/// <remarks>Difference between Linear is that this function will raise the progression by power parameter. This also applies if overshoot / undershoot is true. If current falls behind 0 when undershoot is true, it returns the value when you give the absolute of current - except for it being backwards.</remarks>
	public static float EaseIn(double start, double end, double power, double current, double max, bool overshoot, bool undershoot) {

		float rate = 0.0f;
		// When rate is not integer, rate less than 0 will wreak havoc because it will fall into complex value,
		// which case we will use it's absolute.
		if (!undershoot) {
			rate = current < 0.0f ? 0.0f : (float)Math.Pow((Math.Abs(current) / max), power);
		}
		else {
			rate = (float)Math.Pow((Math.Abs(current) / max), power);
		}
		if (!overshoot) {
			rate = rate > 1.0f ? 1.0f : rate;
		}

		// if rate < 0 when undershoot, move it backwards.
		return (float)(start + (end-start) * rate * (current >= 0 ? 1 : -1));

	}

	/// <summary>
	/// Monomial Shaping Ease Function (Decelerated)
	/// </summary>
	/// <param name="start">Beginning Value</param>
	/// <param name="end">Ending Value</param>
	/// <param name="power">Progression will be raised by reciprocal of this. This can't be 0!</param>
	/// <param name="current">Current Progress (against Max)</param>
	/// <param name="max">Maximum Progress</param>
	/// <param name="overshoot">True to allow the return to exceed ending value, when current > max</param>
	/// <param name="undershoot">True to allow the return to fall behind 0, when current < 0</param>
	/// <returns>Value between start and end, based on the progression</returns>
	/// <remarks>Difference between Linear is that this function will raise the progression by reciprocal of power parameter. This also applies if overshoot / undershoot is true. If current falls behind 0 when undershoot is true, it returns the value when you give the absolute of current - except for it being backwards.</remarks>
	public static float EaseOut(double start, double end, double power, double current, double max, bool overshoot, bool undershoot) {

		if (power == 0.0f) {
			throw new System.ArgumentException("Power cannot be 0!", "power");
		}

		float rate = 0.0f;
		// When rate is not integer, rate less than 0 will wreak havoc because it will fall into complex value,
		// which case we will use it's absolute.
		if (!undershoot) {
			rate = current < 0.0f ? 0.0f : (float)Math.Pow((Math.Abs(current) / max), 1 / power);
		}
		else {
			rate = (float)Math.Pow((Math.Abs(current) / max), 1 / power);
		}
		if (!overshoot) {
			rate = rate > 1.0f ? 1.0f : rate;
		}

		// if rate < 0 when undershoot, move it backwards.
		return (float)(start + (end-start) * rate * (current >= 0 ? 1 : -1));

	}

}
