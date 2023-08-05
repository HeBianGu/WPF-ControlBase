// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;

namespace HeBianGu.Window.Notify
{
    public partial class NotifyWindowBase : WindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(NotifyWindowBase), "S.Window.Notify.Default");

        static NotifyWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyWindowBase), new FrameworkPropertyMetadata(typeof(NotifyWindowBase)));
        }

        public NotifyWindowBase()
        {
            this.BindCommand(Commander.Close, (l, k) =>
            {
                this.CloseAnimation?.Invoke(this);
            });

            this.WindowStartupLocation = WindowStartupLocation.Manual;

            this.Loaded += (l, k) =>
            {
                double screeHeight = SystemParameters.WorkArea.Height;

                double screeWidth = SystemParameters.WorkArea.Width;

                this.Top = screeHeight - this.Height - 2;

                this.Left = screeWidth - this.Width - 5;
            };
        }


    }


}
