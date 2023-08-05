// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.MessageContainer
{

    /// <summary> 自定义导航框架 </summary> 
    public class MessageContainer : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MessageContainer), "S.MessageContainer.Default");

        static MessageContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageContainer), new FrameworkPropertyMetadata(typeof(MessageContainer)));
        }

        public void Add(INotifyMessageItem message)
        {
            this.Items.Add(message);
        }
    }
}
