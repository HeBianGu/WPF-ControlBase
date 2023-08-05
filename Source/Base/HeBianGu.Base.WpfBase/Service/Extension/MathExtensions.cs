// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>Provides extension methods for trigonometric, logarithmic, and other common mathematical functions.</summary>
    public static class MathExtensions
    {
        #region Approximation

        /// <summary>Returns the smallest integer greater than or equal to the specified decimal number.</summary>
        /// <returns>The smallest integer greater than or equal to value.</returns>
        /// <param name="value">A decimal number.</param>
        public static decimal Ceiling(this decimal value) { return Math.Ceiling(value); }
        /// <summary>Returns the smallest integer greater than or equal to the specified double-precision floating-point number.</summary>
        /// <returns>The smallest integer greater than or equal to value. If value is equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NegativeInfinity"/>, or <see cref="F:System.Double.PositiveInfinity"/>, that value is returned.</returns>
        /// <param name="value">A double-precision floating-point number.</param>
        public static double Ceiling(this double value) { return Math.Ceiling(value); }

        /// <summary>Returns the largest integer less than or equal to the specified decimal number.</summary>
        /// <returns>The largest integer less than or equal to value.</returns>
        /// <param name="value">A decimal number.</param>
        public static decimal Floor(this decimal value) { return Math.Floor(value); }
        /// <summary>Returns the largest integer less than or equal to the specified double-precision floating-point number.</summary>
        /// <returns>The largest integer less than or equal to value. If value is equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NegativeInfinity"/>, or <see cref="F:System.Double.PositiveInfinity"/>, that value is returned.</returns>
        /// <param name="value">A double-precision floating-point number.</param>
        public static double Floor(this double value) { return Math.Floor(value); }

        /// <summary>Returns the larger of two 8-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static byte AtLeast(this byte a, byte b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two decimal numbers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <param name="b">The second of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static decimal AtLeast(this decimal a, decimal b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two double-precision floating-point numbers.</summary>
        /// <returns>Parameter a or b, whichever is larger. If a, b, or both a and b are equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NaN"/> is returned.</returns>
        /// <param name="a">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two double-precision floating-point numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double AtLeast(this double a, double b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two single-precision floating-point numbers.</summary>
        /// <returns>Parameter a or b, whichever is larger. If a, or b, or both a and b are equal to <see cref="F:System.Single.NaN"/>, <see cref="F:System.Single.NaN"/> is returned.</returns>
        /// <param name="a">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two single-precision floating-point numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float AtLeast(this float a, float b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two 16-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 16-bit signed integers to compare.</param>
        /// <param name="b">The second of two 16-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static short AtLeast(this short a, short b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two 32-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 32-bit signed integers to compare.</param>
        /// <param name="b">The second of two 32-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int AtLeast(this int a, int b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two 64-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 64-bit signed integers to compare.</param>
        /// <param name="b">The second of two 64-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long AtLeast(this long a, long b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two 8-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static sbyte AtLeast(this sbyte a, sbyte b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two 16-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 16-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ushort AtLeast(this ushort a, ushort b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two 32-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 32-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static uint AtLeast(this uint a, uint b) { return Math.Max(a, b); }
        /// <summary>Returns the larger of two 64-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is larger.</returns>
        /// <param name="a">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 64-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ulong AtLeast(this ulong a, ulong b) { return Math.Max(a, b); }

        /// <summary>Returns the smaller of two 8-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static byte AtMost(this byte a, byte b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two decimal numbers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <param name="b">The second of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static decimal AtMost(this decimal a, decimal b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two double-precision floating-point numbers.</summary>
        /// <returns>Parameter a or b, whichever is smaller. If a, b, or both a and b are equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NaN"/> is returned.</returns>
        /// <param name="a">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two double-precision floating-point numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double AtMost(this double a, double b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two single-precision floating-point numbers.</summary>
        /// <returns>Parameter a or b, whichever is smaller. If a, b, or both a and b are equal to <see cref="F:System.Single.NaN"/>, <see cref="F:System.Single.NaN"/> is returned.</returns>
        /// <param name="a">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two single-precision floating-point numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float AtMost(this float a, float b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two 16-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 16-bit signed integers to compare.</param>
        /// <param name="b">The second of two 16-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static short AtMost(this short a, short b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two 32-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 32-bit signed integers to compare.</param>
        /// <param name="b">The second of two 32-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int AtMost(this int a, int b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two 64-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 64-bit signed integers to compare.</param>
        /// <param name="b">The second of two 64-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long AtMost(this long a, long b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two 8-bit signed integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 8-bit signed integers to compare.</param>
        /// <param name="b">The second of two 8-bit signed integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static sbyte AtMost(this sbyte a, sbyte b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two 16-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 16-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ushort AtMost(this ushort a, ushort b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two 32-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 32-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static uint AtMost(this uint a, uint b) { return Math.Min(a, b); }
        /// <summary>Returns the smaller of two 64-bit unsigned integers.</summary>
        /// <returns>Parameter a or b, whichever is smaller.</returns>
        /// <param name="a">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 64-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ulong AtMost(this ulong a, ulong b) { return Math.Min(a, b); }

        /// <summary>Returns the value constrained inclusively between two 8-bit unsigned integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 8-bit unsigned integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static byte Clamp(this byte value, byte a, byte b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two decimal numbers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        /// <param name="b">The second of two <see cref="T:System.Decimal"/> numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static decimal Clamp(this decimal value, decimal a, decimal b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two double-precision floating-point numbers.</summary>
        /// <returns>A value between a and b inclusively. If a, b, or both a and b are equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NaN"/> is returned.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two double-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two double-precision floating-point numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Clamp(this double value, double a, double b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two single-precision floating-point numbers.</summary>
        /// <returns>A value between a and b inclusively. If a, b, or both a and b are equal to <see cref="F:System.Single.NaN"/>, <see cref="F:System.Single.NaN"/> is returned.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two single-precision floating-point numbers to compare.</param>
        /// <param name="b">The second of two single-precision floating-point numbers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static float Clamp(this float value, float a, float b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two 16-bit signed integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 16-bit signed integers to compare.</param>
        /// <param name="b">The second of two 16-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static short Clamp(this short value, short a, short b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two 32-bit signed integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 32-bit signed integers to compare.</param>
        /// <param name="b">The second of two 32-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int Clamp(this int value, int a, int b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two 64-bit signed integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 64-bit signed integers to compare.</param>
        /// <param name="b">The second of two 64-bit signed integers to compare.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long Clamp(this long value, long a, long b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two 8-bit signed integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 8-bit signed integers to compare.</param>
        /// <param name="b">The second of two 8-bit signed integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static sbyte Clamp(this sbyte value, sbyte a, sbyte b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two 16-bit unsigned integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 16-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 16-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ushort Clamp(this ushort value, ushort a, ushort b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two 32-bit unsigned integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 32-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 32-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static uint Clamp(this uint value, uint a, uint b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }
        /// <summary>Returns the value constrained inclusively between two 64-bit unsigned integers.</summary>
        /// <returns>A value between a and b inclusively.</returns>
        /// <param name="value">The value to restrict between a and b.</param>
        /// <param name="a">The first of two 64-bit unsigned integers to compare.</param>
        /// <param name="b">The second of two 64-bit unsigned integers to compare.</param>
        //[CLSCompliant(false)]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static ulong Clamp(this ulong value, ulong a, ulong b) { return a < b ? Math.Min(Math.Max(value, a), b) : Math.Max(Math.Min(value, a), b); }

        /// <summary>Rounds a decimal value to the nearest integer.</summary>
        /// <returns>The integer nearest parameter value. If value is halfway between two integers, one of which is even and the other odd, then the even number is returned.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal"/>.</exception>
        public static decimal Round(this decimal value) { return Math.Round(value); }
        /// <summary>Rounds a double-precision floating-point value to the nearest integer.</summary>
        /// <returns>The integer nearest value. If value is halfway between two integers, one of which is even and the other odd, then the even number is returned.</returns>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        public static double Round(this double value) { return Math.Round(value); }
        /// <summary>Rounds a decimal value to a specified precision.</summary>
        /// <returns>The number nearest value with a precision equal to decimals. If value is halfway between two numbers, one of which is even and the other odd, then the even number is returned. If the precision of value is less than decimals, then value is returned unchanged.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="decimals">The number of significant decimal places  (precision) in the return value.</param>
        /// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal"/>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">decimals is less than 0 or greater than 28.</exception>
        public static decimal Round(this decimal value, int decimals) { return Math.Round(value, decimals); }
        /// <summary>Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
        /// <returns>The integer nearest value. If value is halfway between two numbers, one of which is even and the other odd, then mode determines which of the two is returned.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal"/>.</exception>
        /// <exception cref="T:System.ArgumentException">mode is not a valid value of <see cref="T:System.MidpointRounding"/>.</exception>
        public static decimal Round(this decimal value, MidpointRounding mode) { return Math.Round(value, mode); }
        /// <summary>Rounds a double-precision floating-point value to the specified precision.</summary>
        /// <returns>The number nearest value with a precision equal to digits. If value is halfway between two numbers, one of which is even and the other odd, then the even number is returned. If the precision of value is less than digits, then value is returned unchanged.</returns>
        /// <param name="digits">The number of significant digits (precision) in the return value.</param>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">digits is less than 0 or greater than 15.</exception>
        public static double Round(this double value, int digits) { return Math.Round(value, digits); }
        /// <summary>Rounds a double-precision floating-point value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
        /// <returns>The integer nearest value. If value is halfway between two integers, one of which is even and the other odd, then mode determines which of the two is returned.</returns>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <exception cref="T:System.ArgumentException">mode is not a valid value of <see cref="T:System.MidpointRounding"/>.</exception>
        public static double Round(this double value, MidpointRounding mode) { return Math.Round(value, mode); }
        /// <summary>Rounds a decimal value to a specified precision. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
        /// <returns>The number nearest value with a precision equal to decimals. If value is halfway between two numbers, one of which is even and the other odd, then mode determines which of the two numbers is returned. If the precision of value is less than decimals, then value is returned unchanged.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <param name="decimals">The number of significant decimal places  (precision) in the return value.</param>
        /// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal"/>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">decimals is less than 0 or greater than 28.</exception>
        /// <exception cref="T:System.ArgumentException">mode is not a valid value of <see cref="T:System.MidpointRounding"/>.</exception>
        public static decimal Round(this decimal value, int decimals, MidpointRounding mode) { return Math.Round(value, decimals, mode); }
        /// <summary>Rounds a double-precision floating-point value to the specified precision. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
        /// <returns>The number nearest value with a precision equal to digits. If value is halfway between two numbers, one of which is even and the other odd, then the mode parameter determines which number is returned. If the precision of value is less than digits, then value is returned unchanged.</returns>
        /// <param name="mode">Specification for how to round value if it is midway between two other numbers.</param>
        /// <param name="digits">The number of significant digits (precision) in the return value.</param>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">digits is less than 0 or greater than 15.</exception>
        /// <exception cref="T:System.ArgumentException">mode is not a valid value of <see cref="T:System.MidpointRounding"/>.</exception>
        public static double Round(this double value, int digits, MidpointRounding mode) { return Math.Round(value, digits, mode); }

        /// <summary>Rounds a decimal value to the next multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static decimal RoundUp(this decimal value, decimal factor) { decimal d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d); }
        /// <summary>Rounds a double-precision floating-point value to the next multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static double RoundUp(this double value, double factor) { double d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d); }
        /// <summary>Rounds a single-precision floating-point value to the next multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static float RoundUp(this float value, float factor) { float d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d); }
        /// <summary>Rounds a 64-bit signed integer to the next multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static long RoundUp(this long value, long factor) { long d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d); }
        /// <summary>Rounds a 32-bit signed integer to the next multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static int RoundUp(this int value, int factor) { int d = (value % factor); return d == 0 ? value : value < 0 ? value - d : value + (factor - d); }
        /// <summary>Rounds a 16-bit signed integer to the next multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static short RoundUp(this short value, short factor) { int d = (value % factor); return (short)(d == 0 ? value : value < 0 ? value - d : value + (factor - d)); }
        /// <summary>Rounds a 8-bit signed integer to the next multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is greater than or equal to value.</returns>
        /// <param name="value">A 8-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static byte RoundUp(this byte value, byte factor) { int d = (value % factor); return (byte)(d == 0 ? value : value + (factor - d)); }

        /// <summary>Rounds a decimal value to the next integer.</summary>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static decimal RoundUp(this decimal value) { return RoundUp(value, 1); }
        /// <summary>Rounds a double-precision floating-point value to the next integer.</summary>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static double RoundUp(this double value) { return RoundUp(value, 1); }
        /// <summary>Rounds a single-precision floating-point value to the next integer.</summary>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static float RoundUp(this float value) { return RoundUp(value, 1); }
        /// <summary>Rounds a 64-bit signed integer to the next integer.</summary>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static long RoundUp(this long value) { return RoundUp(value, 1); }
        /// <summary>Rounds a 32-bit signed integer to the next integer.</summary>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static int RoundUp(this int value) { return RoundUp(value, 1); }
        /// <summary>Rounds a 16-bit signed integer to the next integer.</summary>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static short RoundUp(this short value) { return RoundUp(value, (short)1); }
        /// <summary>Rounds a 8-bit signed integer to the next integer.</summary>
        /// <returns>The nearest integer that is greater than or equal to value.</returns>
        /// <param name="value">A 8-bit signed integer to be rounded.</param>
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly")]
        public static byte RoundUp(this byte value) { return RoundUp(value, (byte)1); }

        /// <summary>Rounds a decimal value to the previous multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        public static decimal RoundDown(this decimal value, decimal factor) { decimal d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d); }
        /// <summary>Rounds a double-precision floating-point value to the previous multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        public static double RoundDown(this double value, double factor) { double d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d); }
        /// <summary>Rounds a single-precision floating-point value to the previous multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        public static float RoundDown(this float value, float factor) { float d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d); }
        /// <summary>Rounds a 64-bit signed integer to the previous multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        public static long RoundDown(this long value, long factor) { long d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d); }
        /// <summary>Rounds a 32-bit signed integer to the previous multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        public static int RoundDown(this int value, int factor) { int d = (value % factor); return d == 0 ? value : value > 0 ? value - d : value - (factor + d); }
        /// <summary>Rounds a 16-bit signed integer to the previous multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        public static short RoundDown(this short value, short factor) { int d = (value % factor); return (short)(d == 0 ? value : value > 0 ? value - d : value - (factor + d)); }
        /// <summary>Rounds an 8-bit unsigned integer to the previous multiple of the specified factor.</summary>
        /// <returns>The nearest multiple of factor that is less than or equal to value.</returns>
        /// <param name="value">An 8-bit unsigned integer to be rounded.</param>
        /// <param name="factor">The factor to round the value to.</param>
        public static byte RoundDown(this byte value, byte factor) { int d = (value % factor); return (byte)(value - d); }

        /// <summary>Rounds a decimal value to the previous integer.</summary>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        /// <param name="value">A decimal number to be rounded.</param>
        public static decimal RoundDown(this decimal value) { return RoundDown(value, 1); }
        /// <summary>Rounds a double-precision floating-point value to the previous integer.</summary>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        /// <param name="value">A double-precision floating-point number to be rounded.</param>
        public static double RoundDown(this double value) { return RoundDown(value, 1); }
        /// <summary>Rounds a single-precision floating-point value to the previous integer.</summary>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        /// <param name="value">A single-precision floating-point number to be rounded.</param>
        public static float RoundDown(this float value) { return RoundDown(value, 1); }
        /// <summary>Rounds a 64-bit signed integer to the previous integer.</summary>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        /// <param name="value">A 64-bit signed integer to be rounded.</param>
        public static long RoundDown(this long value) { return RoundDown(value, 1); }
        /// <summary>Rounds a 32-bit signed integer to the previous integer.</summary>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        /// <param name="value">A 32-bit signed integer to be rounded.</param>
        public static int RoundDown(this int value) { return RoundDown(value, 1); }
        /// <summary>Rounds a 16-bit signed integer to the previous integer.</summary>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        /// <param name="value">A 16-bit signed integer to be rounded.</param>
        public static short RoundDown(this short value) { return RoundDown(value, (short)1); }
        /// <summary>Rounds an 8-bit unsigned integer to the previous integer.</summary>
        /// <returns>The nearest integer that is less than or equal to value.</returns>
        /// <param name="value">An 8-bit unsigned integer to be rounded.</param>
        public static byte RoundDown(this byte value) { return RoundDown(value, (byte)1); }

        /// <summary>Calculates the integral part of a specified decimal number.</summary>
        /// <returns>The integral part of value; that is, the number that remains after any fractional digits have been discarded.</returns>
        /// <param name="value">A number to truncate.</param>
        public static decimal Truncate(this decimal value) { return Math.Truncate(value); }
        /// <summary>Calculates the integral part of a specified double-precision floating-point number.</summary>
        /// <returns>The integral part of value; that is, the number that remains after any fractional digits have been discarded.</returns>
        /// <param name="value">A number to truncate.</param>
        public static double Truncate(this double value) { return Math.Truncate(value); }

        #endregion

        #region Arithmetic

        /// <summary>Returns the absolute value of a <see cref="T:System.Decimal"/> number.</summary>
        /// <returns>A <see cref="T:System.Decimal"/>, x, such that 0 ≤ x ≤ <see cref="F:System.Decimal.MaxValue"/>.</returns>
        /// <param name="value">A number in the range <see cref="F:System.Decimal.MinValue"/> ≤ value ≤ <see cref="F:System.Decimal.MaxValue"/>.</param>
        public static decimal Abs(this decimal value) { return Math.Abs(value); }
        /// <summary>Returns the absolute value of a double-precision floating-point number.</summary>
        /// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="F:System.Double.MaxValue"/>.</returns>
        /// <param name="value">A number in the range <see cref="F:System.Double.MinValue"/> ≤ value ≤ <see cref="F:System.Double.MaxValue"/>.</param>
        public static double Abs(this double value) { return Math.Abs(value); }
        /// <summary>Returns the absolute value of a single-precision floating-point number.</summary>
        /// <returns>A single-precision floating-point number, x, such that 0 ≤ x ≤ <see cref="F:System.Single.MaxValue"/>.</returns>
        /// <param name="value">A number in the range <see cref="F:System.Single.MinValue"/> ≤ value ≤ <see cref="F:System.Single.MaxValue"/>.</param>
        public static float Abs(this float value) { return Math.Abs(value); }
        /// <summary>Returns the absolute value of a 16-bit signed integer.</summary>
        /// <returns>A 16-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.Int16.MaxValue"/>.</returns>
        /// <param name="value">A number in the range <see cref="F:System.Int16.MinValue"/> &lt; value ≤ <see cref="F:System.Int16.MaxValue"/>.</param>
        /// <exception cref="T:System.OverflowException">value equals <see cref="F:System.Int16.MinValue"/>.</exception>
        public static short Abs(this short value) { return Math.Abs(value); }
        /// <summary>Returns the absolute value of a 32-bit signed integer.</summary>
        /// <returns>A 32-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.Int32.MaxValue"/>.</returns>
        /// <param name="value">A number in the range <see cref="F:System.Int32.MinValue"/> &lt; value ≤ <see cref="F:System.Int32.MaxValue"/>.</param>
        /// <exception cref="T:System.OverflowException">value equals <see cref="F:System.Int32.MinValue"/>.</exception>
        public static int Abs(this int value) { return Math.Abs(value); }
        /// <summary>Returns the absolute value of a 64-bit signed integer.</summary>
        /// <returns>A 64-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.Int64.MaxValue"/>.</returns>
        /// <param name="value">A number in the range <see cref="F:System.Int64.MinValue"/> &lt; value ≤ <see cref="F:System.Int64.MaxValue"/>.</param>
        /// <exception cref="T:System.OverflowException">value equals <see cref="F:System.Int64.MinValue"/>.</exception>
        public static long Abs(this long value) { return Math.Abs(value); }
        /// <summary>Returns the absolute value of an 8-bit signed integer.</summary>
        /// <returns>An 8-bit signed integer, x, such that 0 ≤ x ≤ <see cref="F:System.SByte.MaxValue"/>.</returns>
        /// <param name="value">A number in the range <see cref="F:System.SByte.MinValue"/> &lt; value ≤ <see cref="F:System.SByte.MaxValue"/>.</param>
        /// <exception cref="T:System.OverflowException">value equals <see cref="F:System.SByte.MinValue"/>.</exception>
        //[CLSCompliant(false)]
        public static sbyte Abs(this sbyte value) { return Math.Abs(value); }

        /// <summary>Produces the full product of two 32-bit numbers.</summary>
        /// <returns>The <see cref="T:System.Int64"/> containing the product of the specified numbers.</returns>
        /// <param name="a">The first <see cref="T:System.Int32"/> to multiply.</param>
        /// <param name="b">The second <see cref="T:System.Int32"/> to multiply.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long BigMul(this int a, int b) { return Math.BigMul(a, b); }

        /// <summary>Calculates the quotient of two 32-bit signed integers and also returns the remainder in an output parameter.</summary>
        /// <returns>The <see cref="T:System.Int32"/> containing the quotient of the specified numbers.</returns>
        /// <param name="a">The <see cref="T:System.Int32"/> that contains the dividend.</param>
        /// <param name="result">The <see cref="T:System.Int32"/> that receives the remainder.</param>
        /// <param name="b">The <see cref="T:System.Int32"/> that contains the divisor.</param>
        /// <exception cref="T:System.DivideByZeroException">b is zero.</exception>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static int DivRem(this int a, int b, out int result) { return Math.DivRem(a, b, out result); }
        /// <summary>Calculates the quotient of two 64-bit signed integers and also returns the remainder in an output parameter.</summary>
        /// <returns>The <see cref="T:System.Int64"/> containing the quotient of the specified numbers.</returns>
        /// <param name="a">The <see cref="T:System.Int64"/> that contains the dividend.</param>
        /// <param name="result">The <see cref="T:System.Int64"/> that receives the remainder.</param>
        /// <param name="b">The <see cref="T:System.Int64"/> that contains the divisor.</param>
        /// <exception cref="T:System.DivideByZeroException">b is zero.</exception>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static long DivRem(this long a, long b, out long result) { return Math.DivRem(a, b, out result); }

        /// <summary>Returns e raised to the specified power.</summary>
        /// <returns>The number e raised to the power value. If value equals <see cref="F:System.Double.NaN"/> or <see cref="F:System.Double.PositiveInfinity"/>, that value is returned. If value equals <see cref="F:System.Double.NegativeInfinity"/>, 0 is returned.</returns>
        /// <param name="value">A number specifying a power.</param>
        public static double Exp(this double value) { return Math.Exp(value); }

        /// <summary>Returns the remainder resulting from the division of a specified number by another specified number.</summary>
        /// <returns>A number equal to x - (y Q), where Q is the quotient of x / y rounded to the nearest integer (if x / y falls halfway between two integers, the even integer is returned).If x - (y Q) is zero, the value +0 is returned if x is positive, or -0 if x is negative. If y = 0, <see cref="F:System.Double.NaN"/> (Not-A-Number) is returned.</returns>
        /// <param name="y">A divisor.</param>
        /// <param name="x">A dividend.</param>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double IEEERemainder(this double x, double y) { return Math.IEEERemainder(x, y); }

        /// <summary>Returns the natural (base e) logarithm of a specified number.</summary>
        /// <returns>See <see cref="Math.Log"/> for details.</returns>
        /// <param name="value">A number whose logarithm is to be found.</param>
        public static double Log(this double value) { return Math.Log(value); }
        /// <summary>Returns the logarithm of a specified number in a specified base.</summary>
        /// <returns>See <see cref="Math.Log"/> for details.</returns>
        /// <param name="value">A number whose logarithm is to be found.</param>
        /// <param name="newBase">The base of the logarithm.</param>
        public static double Log(this double value, double newBase) { return Math.Log(value, newBase); }

        /// <summary>Returns the base 10 logarithm of a specified number.</summary>
        /// <returns>See <see cref="Math.Log10"/> for details.</returns>
        /// <param name="value">A number whose logarithm is to be found.</param>
        public static double Log10(this double value) { return Math.Log10(value); }

        /// <summary>Returns a specified number raised to the specified power.</summary>
        /// <returns>The number x raised to the power y. See <see cref="Math.Pow"/> for details.</returns>
        /// <param name="y">A double-precision floating-point number that specifies a power.</param>
        /// <param name="x">A double-precision floating-point number to be raised to a power.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Pow(this double x, double y) { return Math.Pow(x, y); }

        /// <summary>Returns a value indicating the sign of a decimal number.</summary>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        /// <param name="value">A signed <see cref="T:System.Decimal"/> number.</param>
        public static int Sign(this decimal value) { return Math.Sign(value); }
        /// <summary>Returns a value indicating the sign of a double-precision floating-point number.</summary>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        /// <param name="value">A signed number.</param>
        /// <exception cref="T:System.ArithmeticException">value is equal to <see cref="F:System.Double.NaN"/>.</exception>
        public static int Sign(this double value) { return Math.Sign(value); }
        /// <summary>Returns a value indicating the sign of a single-precision floating-point number.</summary>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        /// <param name="value">A signed number.</param>
        /// <exception cref="T:System.ArithmeticException">value is equal to <see cref="F:System.Single.NaN"/>.</exception>
        public static int Sign(this float value) { return Math.Sign(value); }
        /// <summary>Returns a value indicating the sign of a 16-bit signed integer.</summary>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        /// <param name="value">A signed number.</param>
        public static int Sign(this short value) { return Math.Sign(value); }
        /// <summary>Returns a value indicating the sign of a 32-bit signed integer.</summary>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        /// <param name="value">A signed number.</param>
        public static int Sign(this int value) { return Math.Sign(value); }
        /// <summary>Returns a value indicating the sign of a 64-bit signed integer.</summary>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        /// <param name="value">A signed number.</param>
        public static int Sign(this long value) { return Math.Sign(value); }
        /// <summary>Returns a value indicating the sign of an 8-bit signed integer.</summary>
        /// <returns>A number indicating the sign of value.Number Description -1 value is less than zero. 0 value is equal to zero. 1 value is greater than zero.</returns>
        /// <param name="value">A signed number.</param>
        //[CLSCompliant(false)]
        public static int Sign(this sbyte value) { return Math.Sign(value); }

        /// <summary>Returns the square root of a specified number.</summary>
        /// <returns><see cref="Math.Sqrt"/> for details.</returns>
        /// <param name="value">A number.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Sqrt(this double value) { return Math.Sqrt(value); }

        #endregion

        #region Comparison

        /// <summary>Determines whether a decimal value is inclusively between two values.</summary>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        /// <param name="value">A decimal value to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this decimal value, decimal a, decimal b) { return a < b ? a <= value && value <= b : b <= value && value <= a; }
        /// <summary>Determines whether a double-precision floating-point value is inclusively between two values.</summary>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        /// <param name="value">A double-precision floating-point value to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this double value, double a, double b) { return a < b ? a <= value && value <= b : b <= value && value <= a; }
        /// <summary>Determines whether a single-precision floating-point value is inclusively between two values.</summary>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        /// <param name="value">A double-precision floating-point value to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this float value, float a, float b) { return a < b ? a <= value && value <= b : b <= value && value <= a; }
        /// <summary>Determines whether a 64-bit signed integer is inclusively between two values.</summary>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        /// <param name="value">A 64-bit signed integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this long value, long a, long b) { return a < b ? a <= value && value <= b : b <= value && value <= a; }
        /// <summary>Determines whether a 32-bit signed integer is inclusively between two values.</summary>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        /// <param name="value">A 32-bit signed integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this int value, int a, int b) { return a < b ? a <= value && value <= b : b <= value && value <= a; }
        /// <summary>Determines whether a 16-bit signed integer is inclusively between two values.</summary>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        /// <param name="value">A 16-bit signed integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this short value, short a, short b) { return a < b ? a <= value && value <= b : b <= value && value <= a; }
        /// <summary>Determines whether an 8-bit unsigned integer is inclusively between two values.</summary>
        /// <returns>A boolean representing whether value is inclusively between a and b.</returns>
        /// <param name="value">An 8-bit unsigned integer to compare.</param>
        /// <param name="a">The first bound to compare value against.</param>
        /// <param name="b">The second bound to compare value against.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static bool IsBetween(this byte value, byte a, byte b) { return a < b ? a <= value && value <= b : b <= value && value <= a; }

        /// <summary>Returns a value indicating whether the specified number evaluates to a value that is not a number (<see cref="F:System.Double.NaN"/>).</summary>
        /// <returns>true if value evaluates to <see cref="F:System.Double.NaN"/>; otherwise, false.</returns>
        /// <param name="value">A double-precision floating-point number. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsNaN(this double value) { return Double.IsNaN(value); }
        /// <summary>Returns a value indicating whether the specified number evaluates to not a number (<see cref="F:System.Single.NaN"/>).</summary>
        /// <returns>true if value evaluates to not a number (<see cref="F:System.Single.NaN"/>); otherwise, false.</returns>
        /// <param name="f">A single-precision floating-point number. </param>
        /// <filterpriority>1</filterpriority>
        public static bool IsNaN(this float value) { return Single.IsNaN(value); }

        /// <summary>Retrieves the value of the specified number, or zero if it's not a number (<see cref="F:System.Double.NaN"/>).</summary>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.Double.NaN"/>; otherwise, 0.0.</returns>
        /// <param name="value">A double-precision floating-point number. </param>
        public static double GetValueOrDefault(this double value) { return Double.IsNaN(value) ? 0.0 : value; }
        /// <summary>Retrieves the value of the specified number, or the specified default value if it's not a number (<see cref="F:System.Double.NaN"/>).</summary>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.Double.NaN"/>; otherwise, the <paramref name="defaultValue"/> parameter.</returns>
        /// <param name="value">A double-precision floating-point number. </param>
        /// <param name="defaultValue">The value to return if <see cref="IsNaN"/> returns true.</param>
        public static double GetValueOrDefault(this double value, double defaultValue) { return Double.IsNaN(value) ? defaultValue : value; }
        /// <summary>Retrieves the value of the specified number, or zero if it's not a number (<see cref="F:System.Single.NaN"/>).</summary>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.Single.NaN"/>; otherwise, 0.0.</returns>
        /// <param name="value">A single-precision floating-point number. </param>
        public static float GetValueOrDefault(this float value) { return Single.IsNaN(value) ? 0.0f : value; }
        /// <summary>Retrieves the value of the specified number, or the specified default value if it's not a number (<see cref="F:System.Single.NaN"/>).</summary>
        /// <returns>The value of the <paramref name="value"/> parameter if it doesn't evaluate to <see cref="F:System.Single.NaN"/>; otherwise, the <paramref name="defaultValue"/> parameter.</returns>
        /// <param name="value">A single-precision floating-point number. </param>
        /// <param name="defaultValue">The value to return if <see cref="IsNaN"/> returns true.</param>
        public static float GetValueOrDefault(this float value, float defaultValue) { return Single.IsNaN(value) ? defaultValue : value; }

        #endregion

        #region Trigonometry

        /// <summary>Returns the angle whose cosine is the specified number.</summary>
        /// <returns>An angle, θ, measured in radians, such that 0 ≤ θ ≤ π -or- <see cref="F:System.Double.NaN"/> if value &lt; -1 or value &gt; 1.</returns>
        /// <param name="value">A number representing a cosine, where -1 ≤ value ≤ 1.</param>
        public static double Acos(this double value) { return Math.Acos(value); }

        /// <summary>Returns the angle whose sine is the specified number.</summary>
        /// <returns>An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2 -or- <see cref="F:System.Double.NaN"/> if value &lt; -1 or value &gt; 1.</returns>
        /// <param name="value">A number representing a sine, where -1 ≤ value ≤ 1.</param>
        public static double Asin(this double value) { return Math.Asin(value); }

        /// <summary>Returns the angle whose tangent is the specified number.</summary>
        /// <returns>An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2 -or- <see cref="F:System.Double.NaN"/> if value equals <see cref="F:System.Double.NaN"/>, -π/2 rounded to double precision (-1.5707963267949) if value equals <see cref="F:System.Double.NegativeInfinity"/>, or π/2 rounded to double precision (1.5707963267949) if value equals <see cref="F:System.Double.PositiveInfinity"/>.</returns>
        /// <param name="value">A number representing a tangent.</param>
        public static double Atan(this double value) { return Math.Atan(value); }

        /// <summary>Returns the angle whose tangent is the quotient of two specified numbers.</summary>
        /// <returns>An angle, θ, measured in radians, such that -π ≤ θ ≤ π, and tan(θ) = y / x, where (x, y) is a point in the Cartesian plane. See <see cref="Math.Atan2"/> for details.</returns>
        /// <param name="y">The y coordinate of a point.</param>
        /// <param name="x">The x coordinate of a point.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Atan2(this double y, double x) { return Math.Atan2(y, x); }

        /// <summary>Returns the cosine of the specified angle.</summary>
        /// <returns>The cosine of angle.</returns>
        /// <param name="angle">An angle, measured in radians.</param>
        public static double Cos(this double angle) { return Math.Cos(angle); }

        /// <summary>Returns the hyperbolic cosine of the specified angle.</summary>
        /// <returns>The hyperbolic cosine of angle. If angle is equal to <see cref="F:System.Double.NegativeInfinity"/> or <see cref="F:System.Double.PositiveInfinity"/>, <see cref="F:System.Double.PositiveInfinity"/> is returned. If angle is equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NaN"/> is returned.</returns>
        /// <param name="angle">An angle, measured in radians.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Cosh(this double angle) { return Math.Cosh(angle); }

        /// <summary>Returns the sine of the specified angle.</summary>
        /// <returns>The sine of angle. If angle is equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NegativeInfinity"/>, or <see cref="F:System.Double.PositiveInfinity"/>, this method returns <see cref="F:System.Double.NaN"/>.</returns>
        /// <param name="angle">An angle, measured in radians.</param>
        public static double Sin(this double angle) { return Math.Sin(angle); }

        /// <summary>Returns the hyperbolic sine of the specified angle.</summary>
        /// <returns>The hyperbolic sine of angle. If angle is equal to <see cref="F:System.Double.NegativeInfinity"/>, <see cref="F:System.Double.PositiveInfinity"/>, or <see cref="F:System.Double.NaN"/>, this method returns a <see cref="T:System.Double"/> equal to angle.</returns>
        /// <param name="angle">An angle, measured in radians.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Sinh(this double angle) { return Math.Sinh(angle); }

        /// <summary>Returns the tangent of the specified angle.</summary>
        /// <returns>The tangent of angle. If angle is equal to <see cref="F:System.Double.NaN"/>, <see cref="F:System.Double.NegativeInfinity"/>, or <see cref="F:System.Double.PositiveInfinity"/>, this method returns <see cref="F:System.Double.NaN"/>.</returns>
        /// <param name="angle">An angle, measured in radians.</param>
        public static double Tan(this double angle) { return Math.Tan(angle); }

        /// <summary>Returns the hyperbolic tangent of the specified angle.</summary>
        /// <returns>The hyperbolic tangent of angle. If angle is equal to <see cref="F:System.Double.NegativeInfinity"/>, this method returns -1. If angle is equal to <see cref="F:System.Double.PositiveInfinity"/>, this method returns 1. If angle is equal to <see cref="F:System.Double.NaN"/>, this method returns <see cref="F:System.Double.NaN"/>.</returns>
        /// <param name="angle">An angle, measured in radians.</param>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public static double Tanh(this double angle) { return Math.Tanh(angle); }

        #endregion

        #region Sequences

        /// <summary>Generates a sequence of decimal numbers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<decimal> To(this decimal start, decimal bound) { decimal step = start <= bound ? 1 : (decimal)-1; for (decimal i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of decimal numbers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<decimal> To(this decimal start, decimal bound, decimal step) { for (decimal i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of double-precision floating-point values within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<double> To(this double start, double bound) { double step = start <= bound ? 1 : (double)-1; for (double i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of double-precision floating-point values within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<double> To(this double start, double bound, double step) { for (double i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of single-precision floating-point values within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<float> To(this float start, float bound) { float step = start <= bound ? 1 : (float)-1; for (float i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of single-precision floating-point values within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<float> To(this float start, float bound, float step) { for (float i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of 64-bit signed integers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<long> To(this long start, long bound) { long step = start <= bound ? 1 : (long)-1; for (long i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of 64-bit signed integers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<long> To(this long start, long bound, long step) { for (long i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of 32-bit signed integers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<int> To(this int start, int bound) { int step = start <= bound ? 1 : -1; for (int i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of 32-bit signed integers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<int> To(this int start, int bound, int step) { for (int i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of 16-bit signed integers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is 1 greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<short> To(this short start, short bound) { short step = start <= bound ? (short)1 : (short)-1; for (short i = start; i < bound; i += step) yield return i; }
        /// <summary>Generates a sequence of 16-bit signed integers within a specified range.</summary>
        /// <returns>A sequence of numbers from start to (but not including) bound where each number is a given step greater than the previous number.</returns>
        /// <param name="start">The number to start from.</param>
        /// <param name="bound">The number to stop before.</param>
        public static IEnumerable<short> To(this short start, short bound, short step) { for (short i = start; i < bound; i += step) yield return i; }

        #endregion
    }
}