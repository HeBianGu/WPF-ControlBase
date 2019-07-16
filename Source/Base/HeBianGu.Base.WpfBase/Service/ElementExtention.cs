using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
    }


}
