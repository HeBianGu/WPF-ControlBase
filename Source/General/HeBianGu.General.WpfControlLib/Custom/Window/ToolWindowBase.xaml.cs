using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 链接主窗口 </summary>
    public partial class ToolWindowBase : MainWindowBase
    {
        public ObservableCollection<LinkAction> TabLinks
        {
            get { return (ObservableCollection<LinkAction>)GetValue(TabLinksProperty); }
            set { SetValue(TabLinksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabLinksProperty =
            DependencyProperty.Register("TabLinks", typeof(ObservableCollection<LinkAction>), typeof(ToolWindowBase), new PropertyMetadata(new ObservableCollection<LinkAction>(), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                ObservableCollection<LinkAction> config = e.NewValue as ObservableCollection<LinkAction>;

            }));

        public LinkGroup SettingLinks
        {
            get { return (LinkGroup)GetValue(SettingLinksProperty); }
            set { SetValue(SettingLinksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SettingLinksProperty =
            DependencyProperty.Register("SettingLinks", typeof(LinkGroup), typeof(LinkWindowBase), new PropertyMetadata(new LinkGroup(), (d, e) =>
            {
                WindowBase control = d as WindowBase;

                if (control == null) return;

                ObservableCollection<Link> config = e.NewValue as ObservableCollection<Link>;

            }));



        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(ToolWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }



        public LinkAction CurrentLink
        {
            get { return (LinkAction)GetValue(CurrentLinkProperty); }
            set { SetValue(CurrentLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentLinkProperty =
            DependencyProperty.Register("CurrentLink", typeof(LinkAction), typeof(ToolWindowBase), new PropertyMetadata(default(LinkAction), (d, e) =>
            {
                ToolWindowBase control = d as ToolWindowBase;

                if (control == null) return;

                LinkAction config = e.NewValue as LinkAction;

            }));


        public ObservableCollection<ClickAction> ClickActions
        {
            get { return (ObservableCollection<ClickAction>)GetValue(ClickActionsProperty); }
            set { SetValue(ClickActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickActionsProperty =
            DependencyProperty.Register("ClickActions", typeof(ObservableCollection<ClickAction>), typeof(ToolWindowBase), new PropertyMetadata(default(ObservableCollection<ClickAction>), (d, e) =>
             {
                 ToolWindowBase control = d as ToolWindowBase;

                 if (control == null) return;

                 ObservableCollection<ClickAction> config = e.NewValue as ObservableCollection<ClickAction>;

             }));

    }


    public class ClickAction
    {
        public string  Logo{ get; set; }

        public string DisplayName { get; set; }

        public Action Action { get; set; }

        public void DoAction()
        {
            this.Action?.Invoke();
        }
    }
}
