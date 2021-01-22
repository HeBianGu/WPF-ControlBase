using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
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
