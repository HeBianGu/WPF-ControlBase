 using System;
using System.Windows; 

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 关闭窗口的动作
    /// </summary>
    public class CloseWindowAction : TriggerAction<FrameworkElement>
    {
        public static readonly DependencyProperty ClosingCheckFuncProperty =
            DependencyProperty.Register("ClosingCheckFunc", typeof(Func<bool>), typeof(CloseWindowAction), new PropertyMetadata(null));

        public Func<bool> ClosingCheckFunc
        {
            get { return (Func<bool>)GetValue(ClosingCheckFuncProperty); }
            set { SetValue(ClosingCheckFuncProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (ClosingCheckFunc != null && !ClosingCheckFunc())
            {
                return;
            }

            var window = AssociatedObject.GetParent<Window>();

            if (window != null)
            {
                window.Close();
            }
        }
    }
}