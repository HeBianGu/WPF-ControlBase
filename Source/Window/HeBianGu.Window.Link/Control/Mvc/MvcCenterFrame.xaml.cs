// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Window.Link
{
    /// <summary> 自定义导航框架 </summary>

    public class MvcCenterFrame : ContentControl
    {
        static MvcCenterFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MvcCenterFrame), new FrameworkPropertyMetadata(typeof(MvcCenterFrame)));
        }



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }


    }

}
