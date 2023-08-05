// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class ScrollToEndListBox : ListBox
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ScrollToEndListBox), "S.ScrollToEndListBox.Default");

        static ScrollToEndListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScrollToEndListBox), new FrameworkPropertyMetadata(typeof(ScrollToEndListBox)));
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            ScrollViewer find = this.GetParent<ScrollViewer>();
            if (find == null)
            {
                this.GetChild<ScrollViewer>()?.ScrollToBottom();
                return;
            }
            find.ScrollToBottom();
        }
    }
}
