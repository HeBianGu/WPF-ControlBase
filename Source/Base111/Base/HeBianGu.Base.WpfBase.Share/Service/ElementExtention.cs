using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    public static class ElementExtention
    {
        #region BindCommand

        /// <summary>
        /// 绑定命令和命令事件到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement @ui, ICommand com, Action<object, ExecutedRoutedEventArgs> call)
        {
            var bind = new CommandBinding(com);
            bind.Executed += new ExecutedRoutedEventHandler(call);
            @ui.CommandBindings.Add(bind);
        }

        /// <summary>
        /// 绑定RelayCommand命令到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement @ui, ICommand com)
        {
            var bind = new CommandBinding(com);
            @ui.CommandBindings.Add(bind);
        }

        #endregion
   
        public static void Visible(this UIElement @ui)
        {
            @ui.Visibility=Visibility.Visible;
        }

        public static void VisibilityWith(this UIElement @ui,bool from)
        {
            if(from)
            {
                @ui.Visible();
            }else
            {
                ui.Collapsed();
            }
        }

        public static void Hidden(this UIElement @ui)
        {
            @ui.Visibility = Visibility.Hidden;
        }

        public static void Collapsed(this UIElement @ui)
        {
            @ui.Visibility = Visibility.Collapsed;
        }


 
    }


}
