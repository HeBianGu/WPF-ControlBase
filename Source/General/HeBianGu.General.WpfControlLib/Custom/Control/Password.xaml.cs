using HeBianGu.Base.WpfBase;
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
    [TemplatePart(Name = "PART_PassWord", Type = typeof(System.Windows.Controls.PasswordBox))]
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public partial class Password : ContentControl
    {
        System.Windows.Controls.PasswordBox _password = null;
        TextBox _textbox = null;
        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this._password = Template.FindName("PART_PassWord", this) as System.Windows.Controls.PasswordBox;

            this._textbox = Template.FindName("PART_TextBox", this) as TextBox;

            this._password.PasswordChanged += pb_center_PasswordChanged;

            this._textbox.TextChanged += BindPassWordBox_TextChanged;

            this._password.Password = this._textbox.Text;
        }

        private void BindPassWordBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.PassWord = this._textbox.Text;
        }

        private void pb_center_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.PassWord = this._password.Password;
        }


        static Password()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Password), new FrameworkPropertyMetadata(typeof(Password)));
        }

        public string PassWord
        {
            get { return (string)GetValue(PassWordProperty); }
            set { SetValue(PassWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.Register("PassWord", typeof(string), typeof(Password), new PropertyMetadata(default(string), (d, e) =>
            {
                Password control = d as Password;

                if (control == null) return;

                string config = e.NewValue as string;

                if (config == null) return;

                if (control._password == null) return;

                control._password.Password = config;

                SetPasswordBoxSelection(control._password, config.Length, 0);
            }));


        private static void SetPasswordBoxSelection(System.Windows.Controls.PasswordBox passwordBox, int start, int length)
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
            DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(Password), new PropertyMetadata(default(Brush), (d, e) =>
            {
                Password control = d as Password;

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
            DependencyProperty.Register("PasswordChar", typeof(string), typeof(Password), new PropertyMetadata(default(string), (d, e) =>
            {
                Password control = d as Password;

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
            DependencyProperty.Register("CaretBrush", typeof(Brush), typeof(Password), new PropertyMetadata(default(Brush), (d, e) =>
            {
                Password control = d as Password;

                if (control == null) return;

                Brush config = e.NewValue as Brush;

            }));

    }
}
