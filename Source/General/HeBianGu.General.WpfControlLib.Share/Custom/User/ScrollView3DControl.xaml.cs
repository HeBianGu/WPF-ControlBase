// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// ScrollView3DControl.xaml 的交互逻辑
    /// </summary>
    public partial class ScrollView3DControl : UserControl
    {
        public ScrollView3DControl()
        {
            InitializeComponent();

            //Set3D(Str);
        }


        //public List<Visual> ItemSource
        //{
        //    get { return (List<Visual>)GetValue(ItemSourceProperty); }
        //    set { SetValue(ItemSourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ItemSourceProperty =
        //    DependencyProperty.Register("ItemSource", typeof(List<Visual>), typeof(ScrollView3DControl), new PropertyMetadata(new List<Visual>(), (d, e) =>
        //     {
        //         //ScrollView3DControl control = d as ScrollView3DControl;

        //         //if (control == null) return;

        //         //List<Visual> config = e.NewValue as List<Visual>;


        //         //if (config == null) return;

        //         //control.Set3D(config);


        //     }));


        private List<Visual> _source;

        public List<Visual> Source
        {
            get { return _source; }
            set { _source = value; }
        }


        public void Draw(List<Visual> items)
        {
            this.Set3D(items);
        }

        private const byte ShowItemCount = 10;

        private double AddAngle = 6;

        private double StartPosition = 0;

        private double AngleOffset = 0;

        private string ItemCount = "0";

        private int ItemCountInt = 0;

        private int SelectItemIndex = 0;

        private void Set3D(List<Visual> items)
        {


            try
            {
                ControlAngle.Angle = 0;

                ItemCountInt = items.Count - 1;

                ItemCount = " / " + items.Count.ToString();

                SelectItemIndex = 0;

                AngleOffset = 360.0 / items.Count;

                double ItemAngle = AngleOffset * Math.PI / 180;

                int OneSideCount = (int)Math.Floor(items.Count / 4.0);

                int VisualWidth = OneSideCount * 35;

                double StartAngle = 0;

                MV.Children.Clear();

                foreach (Visual Item in items)
                {
                    ModelVisual3D MV3D = new ModelVisual3D
                    {
                        Transform = new TranslateTransform3D(Math.Sin(StartAngle) * VisualWidth, 0, Math.Cos(StartAngle) * VisualWidth)
                    };

                    ModelVisual3D MV3DContent = new ModelVisual3D
                    {
                        Content = (Model3DGroup)this.Resources["M3DG"],
                        Transform = (Transform3D)this.Resources["RT"]
                    };

                    //BitmapImage Img = new BitmapImage(new Uri(Item));

                    MV3DContent.Children.Add(new Viewport2DVisual3D
                    {
                        Geometry = (MeshGeometry3D)this.Resources["MG1"],
                        Material = (Material)this.Resources["DM"],

                        Visual = Item
                        //Visual= new TextBlock() { Text="你好"}
                        //Visual = new Button
                        //{
                        //    //Tag = Img,
                        //    Content = ((Button)Item).Content
                        //    //Style = (Style)this.Resources["Btn"]
                        //}

                        //Visual = new Button
                        //{
                        //    //Tag = Img,
                        //    Style = (Style)this.Resources["Btn"]
                        //}
                    });

                    //MV3DContent.Children.Add(new Viewport2DVisual3D
                    //{
                    //    Geometry = (MeshGeometry3D)this.Resources["MG2"],
                    //    Material = (Material)this.Resources["DM"],


                    //    Visual = new Border
                    //    {
                    //        Style = (Style)this.Resources["Bdr"],
                    //        Child = new Border
                    //        {
                    //            //Child = new Image
                    //            //{
                    //            //    Stretch = Stretch.Fill,
                    //            //    Source = Img
                    //            //},

                    //            Child = (UIElement)Item,
                    //            Width = 70,
                    //            Height = 70,
                    //            VerticalAlignment = VerticalAlignment.Bottom
                    //        }
                    //    }
                    //});

                    MV3D.Children.Add(MV3DContent);

                    MV.Children.Add(MV3D);

                    StartAngle = StartAngle + ItemAngle;

                }

                int ShowItemCountTemp = ShowItemCount;

                if (ShowItemCount > OneSideCount)
                {
                    ShowItemCountTemp = OneSideCount;
                }

                double AngleTemp = ItemAngle * ShowItemCountTemp;

                double Temp = (1 - Math.Cos(AngleTemp)) * VisualWidth;

                PC.FarPlaneDistance = 150 + Temp;

                PC.Position = new Point3D(0, 0, 150 + VisualWidth);

                PC.FieldOfView = Math.Atan(Math.Sin(AngleTemp) * (VisualWidth + 250) / PC.FarPlaneDistance) * 360 / Math.PI;

                AddAngle = 360.0 / items.Count / 30.0;

                L1.Position = new Point3D(PC.Position.X - 100, PC.Position.Y + 100, PC.Position.Z);

                L1.Direction = new Vector3D(0 - L1.Position.X, 0 - L1.Position.Y, 0 - L1.Position.Z);

                L1.OuterConeAngle = PC.FieldOfView;

                L2.Position = new Point3D(PC.Position.X + 100, L1.Position.Y, L1.Position.Z);

                L2.Direction = new Vector3D(0 - L2.Position.X, L1.Direction.Y, L1.Direction.Z);

                L2.OuterConeAngle = PC.FieldOfView;
            }
            catch { }
        }


        private void Control_MouseMove(System.Object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SetControlAngle(e.GetPosition(this).X - StartPosition, 1);

                StartPosition = e.GetPosition(this).X;
            }
        }
        private void Control_MouseLeftButtonDown(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StartPosition = e.GetPosition(this).X;
        }
        private void Control_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            SetControlAngle(0 - e.Delta, 5);
        }
        private void SetControlAngle(double i, int j)
        {
            if (i < 0)
            {
                ControlAngle.Angle = ControlAngle.Angle + AddAngle * j;
            }
            else
            {
                if (i > 0)
                {
                    ControlAngle.Angle = ControlAngle.Angle - AddAngle * j;
                }
            }
            if (ControlAngle.Angle < 0)
            {
                ControlAngle.Angle = 360;
            }
            else
            {
                if (ControlAngle.Angle > 360)
                {
                    ControlAngle.Angle = 0;
                }
            }
            SelectItemIndex = (int)Math.Round((ControlAngle.Angle / AngleOffset), 0);
            if (SelectItemIndex > ItemCountInt)
            {
                SelectItemIndex = ItemCountInt;
            }
        }
    }
}
