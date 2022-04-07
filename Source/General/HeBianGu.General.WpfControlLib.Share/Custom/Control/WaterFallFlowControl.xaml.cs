// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class WaterFallFlowControl : Canvas
    {
        private int column;
        private double listWidth = 180;
        public double ListWidth { get { return listWidth; } set { listWidth = value; SetColumn(); } }
        static WaterFallFlowControl()
        {
            //ElementBase.DefaultStyle<WaterFallFlowControl>(DefaultStyleKeyProperty);

            DefaultStyleKeyProperty.OverrideMetadata(typeof(WaterFallFlowControl), new FrameworkPropertyMetadata(typeof(WaterFallFlowControl)));

        }
        public WaterFallFlowControl()
        {
            Loaded += delegate
            {
                SetColumn();
                Margin = new Thickness(Margin.Left);
            };
            SizeChanged += delegate
            {
                SetColumn();
            };
        }

        private void SetColumn()
        {
            // MinWidth = listWidth + Margin.Left * 4;
            column = (int)(ActualWidth / listWidth);
            if (column <= 0) column = 1;
            Refresh();
        }

        public void Add(FrameworkElement element)
        {
            element.Width = ListWidth;

            if (element is Grid)
            {
                if ((element as Grid).Children.Count > 0)
                {
                    ((element as Grid).Children[0] as FrameworkElement).Margin = new Thickness(Margin.Left);
                }
            }
            else if (element is GroupBox)
            {
                (element as GroupBox).Margin = new Thickness(Margin.Left);
            }

            Children.Add(element);
            Refresh();
        }

        public class Point
        {
            public int Index { get; set; }
            public double Buttom { get; set; }
            public double Height { get; set; }
            public Point(int index, double height, double buttom) { Index = index; Height = height; Buttom = buttom; }
        }

        public void Refresh()
        {
            // 初始化参数
            double maxHeight = 0.0;
            Dictionary<int, Point> list = new Dictionary<int, Point>();
            Dictionary<int, Dictionary<int, Point>> nlist = new Dictionary<int, Dictionary<int, Point>>();
            for (int i = 0; i < Children.Count; i++)
            {
                (Children[i] as FrameworkElement).UpdateLayout();
                list.Add(i, new Point(i, (Children[i] as FrameworkElement).ActualHeight
                    + (Children[i] as FrameworkElement).Margin.Bottom
                    + (Children[i] as FrameworkElement).Margin.Top, 0.0));

            }
            for (int i = 0; i < column; i++)
            {
                nlist.Add(i, new Dictionary<int, Point>());
            }

            // 智能排序到 nlist
            for (int i = 0; i < list.Count; i++)
            {
                if (i < column)
                {
                    list[i].Buttom = list[i].Height;
                    nlist[i].Add(nlist[i].Count, list[i]);
                }
                else
                {
                    double b = 0.0;
                    int l = 0;
                    for (int j = 0; j < column; j++)
                    {
                        double jh = nlist[j][nlist[j].Count - 1].Buttom + list[i].Height;
                        if (b == 0.0 || jh < b)
                        {
                            b = jh;
                            l = j;
                        }
                    }
                    list[i].Buttom = b;
                    nlist[l].Add(nlist[l].Count, list[i]);
                }
            }

            // 开始布局
            for (int i = 0; i < nlist.Count; i++)
            {
                for (int j = 0; j < nlist[i].Count; j++)
                {
                    if (Children[nlist[i][j].Index] is Grid)
                    {
                        Children[nlist[i][j].Index].SetValue(LeftProperty, i * ActualWidth / column);
                        Children[nlist[i][j].Index].SetValue(TopProperty, nlist[i][j].Buttom - nlist[i][j].Height);
                        Children[nlist[i][j].Index].SetValue(WidthProperty, ActualWidth / column);

                        ((Children[nlist[i][j].Index] as Grid).Children[0] as FrameworkElement).Margin = Margin;
                    }
                    else if (Children[nlist[i][j].Index] is GroupBox)
                    {
                        Children[nlist[i][j].Index].SetValue(LeftProperty, i * ActualWidth / column);
                        Children[nlist[i][j].Index].SetValue(TopProperty, nlist[i][j].Buttom - nlist[i][j].Height);
                        Children[nlist[i][j].Index].SetValue(WidthProperty, Math.Max(ActualWidth / column - Margin.Left - Margin.Right, 0));
                    }
                }

                // 不知道为什么如果不写这么一句会出错
                if (nlist.ContainsKey(i))
                {
                    if (nlist[i].ContainsKey(nlist[i].Count - 1))
                    {
                        double mh = nlist[i][nlist[i].Count - 1].Buttom;
                        maxHeight = mh > maxHeight ? mh : maxHeight;
                    }
                }
            }
            Height = maxHeight;
            list.Clear();
            nlist.Clear();
        }

        public void Remove(UIElement element)
        {
            Children.Remove(element);
            Refresh();
        }
    }
}
