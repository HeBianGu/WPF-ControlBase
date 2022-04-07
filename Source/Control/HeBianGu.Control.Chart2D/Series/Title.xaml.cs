// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 曲线视图 </summary>
    public class Title : Layer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Title), "S.Title.Default");

        static Title()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Title), new FrameworkPropertyMetadata(typeof(Title)));
        }
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
