using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
