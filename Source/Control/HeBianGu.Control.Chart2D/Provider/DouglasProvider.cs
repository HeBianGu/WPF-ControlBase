// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HeBianGu.Control.Chart2D
{
    public enum ThinningType
    {
        None = 0, Douglas
    }

    internal class DouglasProvider
    {
        // 道格拉斯抽稀算法
        public static List<Point> DouglasThinningMachine(List<Point> spList, double threshold = 10)
        {
            int max = spList.Count;

            //if (max < 500)
            //{
            //    return spList;
            //}

            // 此处限定了距离阈值
            int location = 0;

            // 创建两个栈记录索引值
            Stack<int> A = new Stack<int>();
            Stack<int> B = new Stack<int>();
            A.Push(0);
            B.Push(max - 1);
            do
            {
                double d = FindMostDistance(spList, A.Peek(), B.Peek(), ref location);
                if (d > threshold)
                {
                    B.Push(location);
                }
                else
                {
                    A.Push(location);
                    B.Pop();
                }
            } while (B.Count > 0);
            List<int> listOfIndex = A.ToList();
            listOfIndex.Sort();
            List<Point> result = new List<Point>();
            foreach (int index in listOfIndex)
            {
                result.Add(spList[index]);
            }
            return result;
        }
        // 计算最大距离
        private static double FindMostDistance(List<Point> seriesPoints, int start, int end, ref int location)
        {
            if (end - start <= 1)
            {
                location = end;
                return 0;
            }
            double result = 0;
            Point startPoint = seriesPoints[start];
            Point endPoint = seriesPoints[end];
            for (int i = start + 1; i < end; i++)
            {
                // 调用 MathUtil Class 中的静态方法GetDistanceToLine
                double d = DouglasProvider.GetDistanceToLine(startPoint, endPoint, seriesPoints[i]);
                if (d > result)
                {
                    result = d;
                    location = i;
                }
            }
            return result;
        }

        // 在 MathUtil Class 中

        public static double GetDistanceToLine(double p1x, double p1y, double p2x, double p2y, double refpx, double refpy)
            => Math.Abs(((p2y - p1y) * (refpx - p1x)) - ((refpy - p1y) * (p2x - p1x))) / Math.Pow(((p2y - p1y) * (p2y - p1y)) + ((p2x - p1x) * (p2x - p1x)), 0.5);

        public static double GetDistanceToLine(Point point1, Point point2, Point refPoint)
            => DouglasProvider.GetDistanceToLine(point1.X, point1.Y, point2.X, point2.Y, refPoint.X, refPoint.Y);

    }
    //class DouglasProvider
    //{

    //    public static DouglasProvider Instance = new DouglasProvider();

    //    public List<Point> Do(List<Point> inPoint, double D)
    //    {
    //        if (D == 0) return inPoint;
    //        int n1 = 0;
    //        int n2 = inPoint.Count;

    //        List<Point> outPoint = new List<Point>();

    //        Douglas(inPoint, n1, n2, D, ref outPoint);

    //        return outPoint;
    //    }



    //    private void Douglas(List<Point> InPoints, int n1, int n2, double d, ref List<Point> outPoint)
    //    {
    //        int num = InPoints.Count;
    //        //List<MyPoint> OutPoint = new List<MyPoint>();
    //        int Max = 0;//定义拥有最大距离值的点的编号
    //        line MyLine = new line();
    //        MyLine = parameter(InPoints[n1], InPoints[n2 - 1]);  //得到收尾点连线组成的虚线
    //        double MaxDistance = 0;

    //        for (int i = n1 + 1; i < n2 - 1; i++)
    //        {
    //            double pDistance = distance(InPoints[i], MyLine);
    //            if (pDistance > d && pDistance >= MaxDistance)
    //            {
    //                Max = i;
    //                MaxDistance = pDistance;
    //            }
    //        }
    //        if (Max == 0) //如果最大值距离小于阈值直接返回首位
    //        {
    //            outPoint.Add(InPoints[n1]);
    //            outPoint.Add(InPoints[n2 - 1]);
    //            return;
    //        }
    //        else if (n1 + 1 == Max && n2 - 2 != Max)   //第二个点为最大距离
    //        {
    //            Douglas(InPoints, Max + 1, n2, d, ref outPoint);
    //            outPoint.Add(InPoints[n1]);
    //        }
    //        else if (n2 - 2 == Max && n1 + 1 != Max)  //倒数第二个点为最大距离
    //        {
    //            Douglas(InPoints, 0, Max + 1, d, ref outPoint);
    //            outPoint.Add(InPoints[n2 - 1]);
    //        }
    //        else if (n1 + 1 == Max && n2 - 2 == Max)  //夹中间
    //        {
    //            outPoint.Add(InPoints[Max - 1]);
    //            outPoint.Add(InPoints[Max]);
    //            outPoint.Add(InPoints[Max + 1]);
    //            return;
    //        }
    //        else
    //        {
    //            Douglas(InPoints, n1, Max + 1, d, ref outPoint);
    //            Douglas(InPoints, Max, n2, d, ref outPoint);
    //        }
    //    }

    //    /// <summary>
    //    /// 求直线斜率与截距
    //    /// </summary>
    //    /// <param name="p1"></param>
    //    /// <param name="p2"></param>
    //    /// <returns></returns>
    //    private line parameter(Point p1, Point p2)
    //    {
    //        double k, b;
    //        line newcs = new line();
    //        k = (p2.Y - p1.Y) / (p2.X - p1.X);
    //        b = p1.Y - k * p1.X;
    //        newcs.k = k;
    //        newcs.b = b;
    //        return newcs;
    //    }

    //    /// <summary>
    //    /// 求点到直线的距离
    //    /// </summary>
    //    /// <param name="dot"></param>
    //    /// <param name="cs"></param>
    //    /// <returns></returns>
    //    private double distance(Point dot, line cs)
    //    {
    //        double dis = (Math.Abs(cs.k * dot.X - dot.Y + cs.b)) / Math.Sqrt(cs.k * cs.k + 1);
    //        return dis;
    //    }

    //}

    public struct line//记录直线参数的结构
    {
        public double k;
        public double b;
    }
}
