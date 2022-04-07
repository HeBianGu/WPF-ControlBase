// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    /// <summary>五角星Marker</summary>
    public class T5PointMarker : ShapePointMarker
    {

        /// <summary> 描绘形状 </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {

                //两个圆的半径 和第一个点初始角度  
                //r1 = r / 2.5, r2 = r值的互换确定是正五角星还是倒五角星  
                double r1 = this.Size / 2.5, r2 = this.Size, g = 18;


                double pi = Math.PI;


                List<Point> values = new List<Point>(10);//十个点  


                List<Point> values1 = new List<Point>(5);//(内)外接五个点  


                List<Point> values2 = new List<Point>(5);//(外)内接五个点  


                for (int i = 0; i < 5; i++)
                {
                    //计算10个点的坐标  
                    Point p1 = new Point(r1 * Math.Cos(g * pi / 180), r1 * Math.Sin(g * pi / 180));


                    Point p2 = new Point(r2 * Math.Cos((g + 36) * pi / 180), r2 * Math.Sin((g + 36) * pi / 180));


                    values1.Add(p1);


                    values2.Add(p2);


                    g += 72;
                }
                //左半边：3,4,5,6,7,8  
                //右半边：1,2,3,8,9,10  
                values.Add(values1[0]);//1  
                values.Add(values2[0]);//2  
                values.Add(values1[1]);//3  
                values.Add(values2[1]);//4  
                values.Add(values1[2]);//5  
                values.Add(values2[2]);//6  
                values.Add(values1[3]);//7  
                values.Add(values2[3]);//8  
                values.Add(values1[4]);//9  
                values.Add(values2[4]);//10  

                PointCollection pcollect = new PointCollection();

                foreach (Point item in values)
                {
                    Point p = Point.Add(item, new Vector(this.ScreenPoint.X, this.ScreenPoint.Y));

                    pcollect.Add(p);
                }

                StreamGeometry streamGeom = new StreamGeometry();

                using (StreamGeometryContext context = streamGeom.Open())
                {

                    context.BeginFigure(pcollect[0], true, true);

                    foreach (Point item in pcollect)
                    {
                        context.LineTo(item, true, true);
                    }

                }

                return streamGeom;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawGeometry(Fill, Pen, DefiningGeometry);

        }
    }
}
