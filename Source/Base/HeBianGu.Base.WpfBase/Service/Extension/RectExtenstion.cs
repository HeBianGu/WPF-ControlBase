using System;
using System.Globalization;
#if NET
using System.Reflection.Metadata;
#endif
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public static class RectExtenstion
    {
        public static Point GetCenter(this Rect rect)
        {
            return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        public static Rect ToRect(this System.Windows.Point point, double len)
        {
            return new Rect(point.X - len, point.Y - len, 2 * len, 2 * len);
        }

        /// <summary>
        /// 求投影向量
        /// </summary>
        /// <param name="vector1">直线</param>
        /// <param name="vector2">斜线</param>
        /// <returns></returns>
        public static Vector ToProjectionVector(this Vector vector1, Vector vector2)
        {
            //var perpenddiscular = Vector2.Perpendicular(vector1);
            //perpenddiscular.Normalize();
            //double angle = Vector2.AngleBetween(vector2, perpenddiscular);
            //double distance = vector2.Length;
            //double len = distance * Math.Cos(angle);
            //return perpenddiscular * len;

            var perpenddiscular = new Vector(-vector1.Y, vector1.X);
            perpenddiscular.Normalize();
            double angle = Vector.AngleBetween(vector2, perpenddiscular);
            angle = (angle / 180) * Math.PI;
            double distance = vector2.Length;
            double len = distance * Math.Cos(angle);
            return perpenddiscular * len;
        }

        /// <summary>
        /// 正交向量
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector ToPerpendicular(this Vector vector)
        {
            return new Vector(-vector.Y, vector.X);
        }


        public static double ToAngle(this Vector vector)
        {
            double angle = Math.Atan(vector.Y / vector.X);
            return (angle * 180) / Math.PI;
        }
    }
}
