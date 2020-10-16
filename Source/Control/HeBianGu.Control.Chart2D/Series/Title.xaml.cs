using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 曲线视图 </summary>
    public class Title : Layer
    {

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Title), new PropertyMetadata(default(string), (d, e) =>
             {
                 Title control = d as Title;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(Title), new PropertyMetadata(default(string), (d, e) =>
             {
                 Title control = d as Title;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            //  Do ：显示文本
            Label label = new Label() { Content = Text };

            label.Style = this.LabelStyle;

            canvas.Children.Add(label);
          
        }
    }
}
