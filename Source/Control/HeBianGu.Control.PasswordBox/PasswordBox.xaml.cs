// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.PasswordBox
{
    /// <summary> 可以绑定的密码框 </summary>
    [TemplatePart(Name = "PART_PassWord", Type = typeof(System.Windows.Controls.PasswordBox))]
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    public partial class PasswordBox : ContentControl
    {
        public static ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Dynamic");
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Default");
        public static ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Single");
        public static ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Clear");
        public static ComponentResourceKey LabelKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Label");
        public static ComponentResourceKey LabelClearKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.LabelClear");
        public static ComponentResourceKey LabelSingleKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Sinple.Label");
        public static ComponentResourceKey LabelClearSingleKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Sinple.LabelClear");
        public static ComponentResourceKey ShowKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Show");
        public static ComponentResourceKey CircleKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Circle");
        public static ComponentResourceKey LabelCircleKey => new ComponentResourceKey(typeof(PasswordBox), "S.Password.Circle.Label");

        private System.Windows.Controls.PasswordBox _password = null;
        private TextBox _textbox = null;
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


        static PasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox), new FrameworkPropertyMetadata(typeof(PasswordBox)));
        }

        public string PassWord
        {
            get { return (string)GetValue(PassWordProperty); }
            set { SetValue(PassWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.Register("PassWord", typeof(string), typeof(PasswordBox), new PropertyMetadata(default(string), (d, e) =>
            {
                PasswordBox control = d as PasswordBox;

                if (control == null) return;

                string config = e.NewValue as string;

                if (config == null) return;

                if (control._password == null) return;

                control._password.Password = config;

                SetPasswordBoxSelection(control._password, config.Length, 0);
            }));


        private static void SetPasswordBoxSelection(System.Windows.Controls.PasswordBox passwordBox, int start, int length)
        {
            MethodInfo select = passwordBox.GetType().GetMethod("Select",
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
            DependencyProperty.Register("SelectionBrush", typeof(Brush), typeof(PasswordBox), new PropertyMetadata(default(Brush), (d, e) =>
            {
                PasswordBox control = d as PasswordBox;

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
            DependencyProperty.Register("PasswordChar", typeof(string), typeof(PasswordBox), new PropertyMetadata(default(string), (d, e) =>
            {
                PasswordBox control = d as PasswordBox;

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
            DependencyProperty.Register("CaretBrush", typeof(Brush), typeof(PasswordBox), new PropertyMetadata(default(Brush), (d, e) =>
            {
                PasswordBox control = d as PasswordBox;

                if (control == null) return;

                Brush config = e.NewValue as Brush;

            }));

    }
}
