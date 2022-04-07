// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.ImagePlayer
{

    /// <summary>
    /// 矩形图基类
    /// </summary>
    public class RectangleShape : Shape, IRectangleStroke
    {

        public string Code { get; set; }
        public new string Name { get; set; }

        public double NormalStrokeThickness { get; set; } = 5;

        public double MouseOverStrokeThickness { get; set; } = 8;

        public double SelectedStrokeThickness { get; set; } = 10;

        public double SeletedAndMouseOverStrokeThickness { get; set; } = 15;
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public RectangleShape() : base()
        {
            this.InitComponent();

            this.StrokeThickness = this.NormalStrokeThickness;
        }

        private void RefreshStrokeThickness()
        {
            if ((this is DynamicShape)) return;

            if (this.IsSelected)
            {
                if (this.IsMouseOver)
                {
                    this.StrokeThickness = SeletedAndMouseOverStrokeThickness;// this.NormalStrokeThickness * 5 * 8;
                }
                else
                {
                    this.StrokeThickness = SelectedStrokeThickness;// this.NormalStrokeThickness * 5 * 5;
                }

                this.Fill = new SolidColorBrush() { Color = ((SolidColorBrush)this.Fill).Color, Opacity = 0.5 };
            }
            else
            {

                this.Fill = new SolidColorBrush() { Color = ((SolidColorBrush)this.Fill).Color, Opacity = 0.2 };

                if (this.IsMouseOver)
                {
                    this.StrokeThickness = MouseOverStrokeThickness;// this.NormalStrokeThickness * 5 * 3;
                }
                else
                {
                    this.StrokeThickness = NormalStrokeThickness;// this.NormalStrokeThickness * 5;
                }
            }
        }

        public event Action<RectangleShape> Selected;


        public bool IsSelected { get; private set; }

        public void SetSelected()
        {
            InkCanvas canvas = this.Parent as InkCanvas;

            foreach (object item in canvas.Children)
            {
                if (item is RectangleShape)
                {
                    RectangleShape shape = item as RectangleShape;

                    if (shape.IsSelected)
                    {
                        //shape.Fill = new SolidColorBrush() { Color = ((SolidColorBrush)shape.Fill).Color, Opacity = 0.1 };
                        //shape.Stroke = new SolidColorBrush() { Color = ((SolidColorBrush)shape.Stroke).Color, Opacity = 1 };
                        //shape.StrokeThickness /= 3;
                        shape.IsSelected = false;


                        Debug.WriteLine("取消选中：" + shape.Code);


                        shape.RefreshStrokeThickness();
                    }
                }
            }

            this.IsSelected = true;

            //  Message：触发选中事件
            this.Selected?.Invoke(this);

            //this.Fill = new SolidColorBrush() { Color = ((SolidColorBrush)this.Fill).Color, Opacity = 0.7 };
            //this.StrokeThickness *= 3;
            Debug.WriteLine("选中：" + this.Code);
            this.RefreshStrokeThickness();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitComponent()
        {
            this.NormalStrokeThickness = this.StrokeThickness;


            //  Do：鼠标进入事件
            this.MouseEnter += (l, k) =>
            {
                this.RefreshStrokeThickness();
            };

            //  Do：鼠标移除事件
            this.MouseLeave += (l, k) =>
            {

                this.RefreshStrokeThickness();
            };

            this.PreviewMouseDown += (l, k) =>
              {
                  SetSelected();
              };


        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="start"> 左上角点 </param>
        /// <param name="end"> 右下角点 </param>
        public RectangleShape(Point start, Point end)
        {
            this.InitComponent();

            this.Width = Math.Abs(start.X - end.X);
            this.Height = Math.Abs(start.Y - end.Y);

            Position = new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));


            //Debug.WriteLine(this.Width + "*" + this.Height);
            //Debug.WriteLine(Position.X + "*" + Position.Y);

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rectangle"> 矩形形状 </param>
        public RectangleShape(RectangleShape rectangle)
        {
            this.InitComponent();

            this.Width = rectangle.Width;
            this.Height = rectangle.Height;

            this.Position = new Point(rectangle.Position.X, rectangle.Position.Y);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"> 左上方 x坐标 </param>
        /// <param name="y"> 左上方 y坐标</param>
        /// <param name="width"> 宽度 </param>
        /// <param name="height"> 高度 </param>
        public RectangleShape(double x, double y, double width, double height)
        {
            this.InitComponent();

            this.Width = width;
            this.Height = height;

            this.Position = new Point(x, y);
        }


        /// <summary>
        /// 左上方点坐标
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// 矩形区域形状
        /// </summary>
        public Rect Rect
        {
            get
            {
                return new Rect(this.Position, new Size(this.Width,
                     this.Height));
            }
        }

        /// <summary>
        /// 定义模型
        /// </summary>
        protected override Geometry DefiningGeometry
        {
            get
            {
                RectangleGeometry geo = new RectangleGeometry();

                geo.Rect = new Rect(new Point(0, 0), new Size(this.Width,
                    this.Height));
                return geo;
            }
        }

        /// <summary>
        /// 渲染模型
        /// </summary>
        public override Geometry RenderedGeometry
        {
            get
            {
                RectangleGeometry geo = new RectangleGeometry();

                geo.Rect = new Rect(new Point(0, 0), new Size(this.Width,
                    this.Height));
                return geo;
            }
        }

        /// <summary>
        /// 绘制图形
        /// </summary>
        /// <param name="canvas"></param>
        public virtual void Draw(InkCanvas canvas)
        {
            InkCanvas.SetLeft(this, Position.X);
            InkCanvas.SetTop(this, Position.Y);
            canvas.Children.Add(this);
        }

        /// <summary>
        /// 清理图形
        /// </summary>
        /// <param name="canvas"></param>
        public void Clear(InkCanvas canvas)
        {
            canvas.Children.Remove(this);
        }

        public void Clear()
        {
            InkCanvas canvas = this.Parent as InkCanvas;

            canvas.Children.Remove(this);
        }
    }

    public class DataRectangleShape : RectangleShape
    {
        #region - 构造函数 -
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataRectangleShape() : base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataRectangleShape(Point start, Point end) : base(start, end)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataRectangleShape(RectangleShape rectangle) : base(rectangle)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataRectangleShape(double x, double y, double width, double height) : base(x, y, width, height)
        {

        }
        #endregion

    }


    /// <summary>
    /// 动态变换的矩形形状
    /// </summary>
    public class DynamicShape : RectangleShape
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DynamicShape() : base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public DynamicShape(Point start, Point end) : base(start, end)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rectangle"></param>
        public DynamicShape(RectangleShape rectangle) : base(rectangle)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public DynamicShape(double x, double y, double width, double height) : base(x, y, width, height)
        {

        }

        /// <summary>
        /// 鼠标移动时刷新图形
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void Refresh(Point start, Point end)
        {



            this.Width = Math.Abs(start.X - end.X);
            this.Height = Math.Abs(start.Y - end.Y);

            this.Position = new Point(Math.Min(start.X, end.X), Math.Min(start.Y, end.Y));
            InkCanvas.SetLeft(this, Position.X);
            InkCanvas.SetTop(this, Position.Y);

            //Debug.WriteLine(this.Width + "*" + this.Height);
            //Debug.WriteLine(Position.X + "*" + Position.Y);
        }


        /// <summary>
        /// 匹配的高度
        /// </summary>
        public int HeightMatch
        {
            get { return (int)GetValue(HeightMatchProperty); }
            set { SetValue(HeightMatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightMatchProperty =
            DependencyProperty.Register("HeightMatch", typeof(int), typeof(DynamicShape), new PropertyMetadata(10, (d, e) =>
             {
                 DynamicShape control = d as DynamicShape;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        /// <summary>
        /// 匹配的宽度
        /// </summary>
        public int WidthMatch
        {
            get { return (int)GetValue(WidthMatchProperty); }
            set { SetValue(WidthMatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthMatchProperty =
            DependencyProperty.Register("WidthMatch", typeof(int), typeof(DynamicShape), new PropertyMetadata(10, (d, e) =>
             {
                 DynamicShape control = d as DynamicShape;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));


        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <returns></returns>
        public bool IsMatch()
        {
            return _initFlag && this.Width > this.WidthMatch && this.Height > this.HeightMatch;
        }

        private bool _initFlag = false;

        /// <summary>
        /// 通过_initFlag标识是否开始计算匹配
        /// </summary>
        /// <param name="flag"></param>
        public void BegionMatch(bool flag)
        {
            this.Width = 0;
            this.Height = 0;
            _initFlag = flag;
        }

    }

    /// <summary>
    /// 缺陷形状
    /// </summary>
    public class DefectShape : DataRectangleShape
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DefectShape() : base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefectShape(Point start, Point end) : base(start, end)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefectShape(RectangleShape rectangle) : base(rectangle)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefectShape(double x, double y, double width, double height) : base(x, y, width, height)
        {

        }
    }

    /// <summary>
    /// 样本形状
    /// </summary>
    public class SampleShape : DataRectangleShape
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleShape() : base()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleShape(Point start, Point end) : base(start, end)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleShape(RectangleShape rectangle) : base(rectangle)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SampleShape(double x, double y, double width, double height) : base(x, y, width, height)
        {

        }
    }

}
