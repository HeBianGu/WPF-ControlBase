// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageContainer;
using HeBianGu.General.WpfControlLib;

namespace HeBianGu.Window.Notify
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyMessageWindow : NotifyWindowBase
    {
        public NotifyMessageWindow()
        {
            InitializeComponent();

            this.ShowAnimation = l =>
            {

            };

            this.CloseAnimation = l =>
            {
                this.Close();
            };
        }

        public void Add(INotifyMessageItem message)
        {
            this.contrainer.Add(message);
        }
    }
}
