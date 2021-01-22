using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 自定义导航框架 </summary>

    public class MvcAddFrame : ContentControl
    {
        static MvcAddFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MvcAddFrame), new FrameworkPropertyMetadata(typeof(MvcAddFrame)));
        }



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }


    }

}
