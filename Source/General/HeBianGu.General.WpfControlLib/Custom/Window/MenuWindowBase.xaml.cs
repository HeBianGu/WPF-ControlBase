using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 链接主窗口 </summary>
    public partial class MenuWindowBase : MainWindowBase
    {
        public object MenuContent
        {
            get { return (object)GetValue(MenuContentProperty); }
            set { SetValue(MenuContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuContentProperty =
            DependencyProperty.Register("MenuContent", typeof(object), typeof(MenuWindowBase), new PropertyMetadata(default(object), (d, e) =>
             {
                 MenuWindowBase control = d as MenuWindowBase;

                 if (control == null) return;

                 object config = e.NewValue as object;

             }));

    }
}
