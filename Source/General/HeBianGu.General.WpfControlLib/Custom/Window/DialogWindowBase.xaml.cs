// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Input;

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
            this.BindCommand(Commander.Close, (l, k) =>
            {
                this.Result = false;
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
