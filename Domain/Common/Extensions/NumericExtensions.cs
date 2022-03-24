using System;

namespace Domain.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for numeric operations.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="val">The value in degrees.</param>
        /// <returns>The value in radians.</returns>
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
