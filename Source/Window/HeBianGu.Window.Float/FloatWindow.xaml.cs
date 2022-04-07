// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace HeBianGu.Window.Float
{
    public partial class FloatWindow
    {
        public FloatWindow()
        {
            InitializeComponent();
        }

        public static void ShowDefault(params IconFloatModel[] acts)
        {
            FloatWindow m = new FloatWindow();

            m.Source = acts;

            m.Show();
        }


        public static void ShowCircle(params IconFloatModel[] acts)
        {
            FloatWindow m = new FloatWindow();

            m.Source = acts;

            m.Style = Application.Current.FindResource("S.Window.Float.Circle") as Style;

            m.Show();
        }

        public static void ShowWith(Action<FloatWindow> init, params IconFloatModel[] acts)
        {
            FloatWindow m = new FloatWindow();

            m.Source = acts;

            init?.Invoke(m);

            m.Show();
        }


        public static void ShowStyle(Style style, params IconFloatModel[] acts)
        {
            FloatWindow m = new FloatWindow();

            m.Source = acts;

            m.Style = style ?? m.Style;

            m.Show();
        }
    }

}
