// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls.Primitives;

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

            Window window = AssociatedObject.GetParent<Window>();

            if (window != null)
            {
                window.Close();
            }
        }
    }
}