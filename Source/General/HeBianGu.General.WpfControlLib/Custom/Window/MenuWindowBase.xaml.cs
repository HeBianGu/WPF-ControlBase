using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
