// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace HeBianGu.Window.Link
{

    /// <summary> 链接主窗口 </summary>
    public partial class LinkActionsManagerWindowBase : ManagerWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LinkActionsManagerWindowBase), "S.Window.Manager.LinkActions.Default");

        static LinkActionsManagerWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkActionsManagerWindowBase), new FrameworkPropertyMetadata(typeof(LinkActionsManagerWindowBase)));
        }
        //public LinkActionsManagerWindowBase()
        //{
        //    //this.Loaded += (l, k) =>
        //    //{
        //    //    this.CurrentLink = this.LinkActions?.FirstOrDefault();
        //    //};
        //}

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (this.CurrentLink != null) return;

            this.CurrentLink = this.LinkActions?.FirstOrDefault();
        }

        public ObservableCollection<IAction> LinkActions
        {
            get { return (ObservableCollection<IAction>)GetValue(LinkActionsProperty); }
            set { SetValue(LinkActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionsProperty =
            DependencyProperty.Register("LinkActions", typeof(ObservableCollection<IAction>), typeof(LinkActionsManagerWindowBase), new PropertyMetadata(new ObservableCollection<IAction>(), (d, e) =>
            {
                LinkActionsManagerWindowBase control = d as LinkActionsManagerWindowBase;

                if (control == null) return;

                ObservableCollection<IAction> config = e.NewValue as ObservableCollection<IAction>;

            }));
    }
}
