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
        public static new ComponentResourceKey DynamicKey => new ComponentResourceKey(typeof(DialogWindowBase), "S.Window.Dialog.Dynamic");

        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DialogWindowBase), "S.Window.Dialog.Default");
        public static new ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(DialogWindowBase), "S.Window.Dialog.Single");
        public static new ComponentResourceKey AccentKey => new ComponentResourceKey(typeof(DialogWindowBase), "S.Window.Dialog.Accent");
        public static new ComponentResourceKey ClearKey => new ComponentResourceKey(typeof(DialogWindowBase), "S.Window.Dialog.Clear");


        static DialogWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogWindowBase), new FrameworkPropertyMetadata(typeof(DialogWindowBase)));

            StyleLoader.Instance?.LoadDefault(typeof(DialogWindowBase));
        }

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
