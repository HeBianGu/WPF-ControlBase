using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 可以绑定的密码框 </summary>
    [TemplatePart(Name = "PART_PassWord", Type = typeof(PasswordBox))]
    public partial class BindPassWordBox : UserControl
    {
        PasswordBox _password = null;

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this._password = Template.FindName("PART_PassWord", this) as PasswordBox;

            this._password.PasswordChanged += pb_center_PasswordChanged;
        }


        private void pb_center_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.PassWord = this._password.Password;
        }


        static BindPassWordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BindPassWordBox), new FrameworkPropertyMetadata(typeof(BindPassWordBox)));
        }

        public string PassWord
        {
            get { return (string)GetValue(PassWordProperty); }
            set { SetValue(PassWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.Register("PassWord", typeof(string), typeof(BindPassWordBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 BindPassWordBox control = d as BindPassWordBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

                 if (control._password == null) return;

                 control._password.Password = config;

                 //var lable = Base.WpfBase.ControlAttachProperty.GetLabel(control);

                 //Base.WpfBase.ControlAttachProperty.SetLabel(control.pb_center, lable);

                 //var Watermark = Base.WpfBase.ControlAttachProperty.GetWatermark(control);

                 //Base.WpfBase.ControlAttachProperty.SetWatermark(control.pb_center, Watermark);

                 SetPasswordBoxSelection(control._password, config.Length, 0);
             }));


        private static void SetPasswordBoxSelection(PasswordBox passwordBox, int start, int length)
        {
            var select = passwordBox.GetType().GetMethod("Select",
                            BindingFlags.Instance | BindingFlags.NonPublic);

            select.Invoke(passwordBox, new object[] { start, length });
        }




        public Brush SelectionBrush
        {
            get { return (Brush)GetValue(SelectionBrushProperty); }
            set { SetValue(SelectionBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionBrushProperty =
            DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(BindPassWordBox), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 BindPassWordBox control = d as BindPassWordBox;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));


        public string PasswordChar
        {
            get { return (string)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(string), typeof(BindPassWordBox), new PropertyMetadata(default(string), (d, e) =>
             {
                 BindPassWordBox control = d as BindPassWordBox;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public Brush CaretBrush
        {
            get { return (Brush)GetValue(CaretBrushProperty); }
            set { SetValue(CaretBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaretBrushProperty =
            DependencyProperty.Register("CaretBrush", typeof(Brush), typeof(BindPassWordBox), new PropertyMetadata(default(Brush), (d, e) =>
             {
                 BindPassWordBox control = d as BindPassWordBox;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));

    }
}
