using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    static class LinearGradientHelper
    {
        public static void ComputePoints(double controlWidth, double controlHeight,
            double x, double y, double r1, double r2, CornerOrigin fromCorner,
            out Point startPoint, out Point endPoint)
        {
            double b = x;
            double a = -b / y;
            double a2 = -a;

            double xc1 = b / (1 - a * a2);
            double yc1 = a2 * xc1;

            double OC1 = Math.Sqrt(xc1 * xc1 + yc1 * yc1);

            double xc2 = (xc1 * (r1 + r2)) / OC1 + xc1;
            double yc2 = (yc1 * (r1 + r2)) / OC1 + yc1;

            switch (fromCorner)
            {
                case CornerOrigin.TopRight:
                    xc1 = controlWidth - xc1;
                    xc2 = controlWidth - xc2;
                    break;
                case CornerOrigin.BottomLeft:
                    yc1 = controlHeight - yc1;
                    yc2 = controlHeight - yc2;
                    break;
                case CornerOrigin.BottomRight:
                    xc1 = controlWidth - xc1;
                    xc2 = controlWidth - xc2;
                    yc1 = controlHeight - yc1;
                    yc2 = controlHeight - yc2;
                    break;
            }

            startPoint = new Point(xc1 / controlWidth, yc1 / controlHeight);
            endPoint = new Point(xc2 / controlWidth, yc2 / controlHeight);
        }
        public static void ComputePointsFromTop(double controlWidth, double controlHeight,
            double x, double y, double r1, double r2, CornerOrigin fromCorner,
            out Point startPoint, out Point endPoint)
        {
            double b = x;
            double a = -b / y;
            double a2 = -a;

            //double xc1 = b / 2;
            //double yc1 = -b / (2 * a);

            double xc1 = b / (1 - a * a2);
            double yc1 = a2 * xc1;

            double OC1 = Math.Sqrt(xc1 * xc1 + yc1 * yc1);

            double xc2 = xc1 - (xc1 * (r1 + r2)) / OC1;
            double yc2 = yc1 - (yc1 * (r1 + r2)) / OC1;

            switch (fromCorner)
            {
                case CornerOrigin.TopRight:
                    xc1 = controlWidth - xc1;
                    xc2 = controlWidth - xc2;
                    break;
                case CornerOrigin.BottomLeft:
                    yc1 = controlHeight - yc1;
                    yc2 = controlHeight - yc2;
                    break;
                case CornerOrigin.BottomRight:
                    xc1 = controlWidth - xc1;
                    xc2 = controlWidth - xc2;
                    yc1 = controlHeight - yc1;
                    yc2 = controlHeight - yc2;
                    break;
            }

            startPoint = new Point(xc1 / controlWidth, yc1 / controlHeight);
            endPoint = new Point(xc2 / controlWidth, yc2 / controlHeight);
        }
    }
}
