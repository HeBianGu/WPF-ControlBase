// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Window.Link
{
    /// <summary> 自定义导航框架 </summary>

    public class MvcListFrame : ContentControl
    {
        static MvcListFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MvcListFrame), new FrameworkPropertyMetadata(typeof(MvcListFrame)));
        }



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

    }

}
