using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public partial class LoginWindowBase : WindowBase
    {

        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(LoginWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }



        public bool IsLogined
        {
            get { return (bool)GetValue(IsLoginedProperty); }
            set { SetValue(IsLoginedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoginedProperty =
            DependencyProperty.Register("IsLogined", typeof(bool), typeof(LoginWindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 LoginWindowBase control = d as LoginWindowBase;

                 if (control == null) return;

                 bool config = (bool)e.NewValue;

                 if (config)
                 {
                     CloseStoryService.Instance.ScaleReduce(control, new Point(0.5, 0.5), 0.5, 5, () =>
                     {
                         control.DialogResult = true;
                         control.Close();
                     });
                 }

             })); 
       
    }
}
