using System;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Provides extension methods for rects.
    /// </summary>
	public static class RectExtensions
	{
        /// <summary>
        /// Returns the center point of the <see cref="Rect"/>.
        /// </summary>
        /// <param name="rect">The rect to return the center point of.</param>
        /// <returns>The center <see cref="Point"/> of the <paramref name="rect"/>.</returns>
        public static Point GetCenter(this Rect rect)
        {
            return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
        }

        /// <summary>
        /// Returns whether the <see cref="Rect"/> defines a real area in space.
        /// </summary>
        /// <param name="rect">The rect to test.</param>
        /// <returns><c>true</c> if rect defines an area or point in finite space, which is not the case for <see cref="Rect.Empty"/> or if any of the fields are <see cref="double.NaN"/>.</returns>
        public static bool IsDefined(this Rect rect)
        {
            return rect.Width >= 0.0
                && rect.Height >= 0.0
                && rect.Top < Double.PositiveInfinity
                && rect.Left < Double.PositiveInfinity
                && (rect.Top > Double.NegativeInfinity || rect.Height == Double.PositiveInfinity)
                && (rect.Left > Double.NegativeInfinity || rect.Width == Double.PositiveInfinity);
        }

        /// <summary>
        /// Indicates whether the specified rectangle intersects with the current rectangle, properly considering the empty rect and infinities.
        /// </summary>
        /// <param name="self">The current rectangle.</param>
        /// <param name="rect">The rectangle to check.</param>
        /// <returns><c>true</c> if the specified rectangle intersects with the current rectangle; otherwise, <c>false</c>.</returns>
        public static bool Intersects(this Rect self, Rect rect)
        {
            return (self.IsEmpty || rect.IsEmpty)
                || (self.Width == Double.PositiveInfinity || self.Right >= rect.Left)
                && (rect.Width == Double.PositiveInfinity || rect.Right >= self.Left)
                && (self.Height == Double.PositiveInfinity || self.Bottom >= rect.Top)
                && (rect.Height == Double.PositiveInfinity || rect.Bottom >= self.Top);
        }
	}
}