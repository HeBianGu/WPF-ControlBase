using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 自定义导航框架 </summary>
    [TemplatePart(Name = "PART_FTabControl", Type = typeof(TabControl))]

    public class LinkActionTabFrame : ContentControl
    {

        TabControl fTabControl = null;
        static LinkActionTabFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkActionTabFrame), new FrameworkPropertyMetadata(typeof(LinkActionTabFrame)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            fTabControl = GetTemplateChild("PART_FTabControl") as TabControl;
        }

        public ILinkActionBase LinkAction
        {
            get { return (ILinkActionBase)GetValue(LinkActionProperty); }
            set { SetValue(LinkActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionProperty =
            DependencyProperty.Register("LinkAction", typeof(ILinkActionBase), typeof(LinkActionTabFrame), new PropertyMetadata(default(LinkAction), (d, e) =>
            {
                LinkActionTabFrame control = d as LinkActionTabFrame;

                if (control == null) return;

                if (e.NewValue is ILinkActionBase)
                {
                    ILinkActionBase config = e.NewValue as ILinkActionBase;

                    //control.RefreshLinkAction(config);

                    control.RefreshActions(config);
                }

            }));

        void RefreshActions(ILinkActionBase linkAction)
        {
            if (linkAction == null) return;

            if (this.LinkActions.Contains(linkAction)) return;

            this.LinkActions.Add(linkAction);
        }



        public ObservableCollection<ILinkActionBase> LinkActions
        {
            get { return (ObservableCollection<ILinkActionBase>)GetValue(LinkActionsProperty); }
            set { SetValue(LinkActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionsProperty =
            DependencyProperty.Register("LinkActions", typeof(ObservableCollection<ILinkActionBase>), typeof(LinkActionTabFrame), new PropertyMetadata(new ObservableCollection<ILinkActionBase>(), (d, e) =>
             {
                 LinkActionTabFrame control = d as LinkActionTabFrame;

                 if (control == null) return;

                 ObservableCollection<ILinkActionBase> config = e.NewValue as ObservableCollection<ILinkActionBase>;

             }));


    }

}
