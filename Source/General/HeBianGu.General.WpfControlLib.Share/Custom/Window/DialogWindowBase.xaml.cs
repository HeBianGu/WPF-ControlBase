using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
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
    public partial class DialogWindowBase : WindowBase
    {
        public DialogWindowBase()
        {
            this.BindCommand(CommandService.Close, (l, k) =>
            {
                this.CloseAnimation?.Invoke(this);
            });

            this.MouseDown += DialogWindow_MouseDown;
        }

        private void DialogWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public bool Result { get; set; }
    }
}
