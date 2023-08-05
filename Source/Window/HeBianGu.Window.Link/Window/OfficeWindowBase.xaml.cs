// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Window.Message;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Window.Link
{
    public partial class OfficeWindowBase : MessageWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(OfficeWindowBase), "S.Window.Office.Default");

        static OfficeWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OfficeWindowBase), new FrameworkPropertyMetadata(typeof(OfficeWindowBase)));
        }

        public OfficeWindowBase()
        {
            this.Loaded += (l, k) =>
              {
                  if (this.CurrentLink != null) return;

                  this.CurrentLink = this.LinkActions?.FirstOrDefault();
              };
        }


        public ControlTemplate Tab
        {
            get { return (ControlTemplate)GetValue(TabProperty); }
            set { SetValue(TabProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabProperty =
            DependencyProperty.Register("Tab", typeof(ControlTemplate), typeof(OfficeWindowBase), new FrameworkPropertyMetadata(default(ControlTemplate), (d, e) =>
             {
                 OfficeWindowBase control = d as OfficeWindowBase;

                 if (control == null) return;

                 if (e.OldValue is ControlTemplate o)
                 {

                 }

                 if (e.NewValue is ControlTemplate n)
                 {

                 }

             }));


        public ObservableCollection<IAction> LinkActions
        {
            get { return (ObservableCollection<IAction>)GetValue(LinkActionsProperty); }
            set { SetValue(LinkActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionsProperty =
            DependencyProperty.Register("LinkActions", typeof(ObservableCollection<IAction>), typeof(OfficeWindowBase), new PropertyMetadata(new ObservableCollection<IAction>(), (d, e) =>
            {
                OfficeWindowBase control = d as OfficeWindowBase;

                if (control == null) return;

                ObservableCollection<IAction> config = e.NewValue as ObservableCollection<IAction>;



            }));

        public IAction CurrentLink
        {
            get { return (IAction)GetValue(CurrentLinkProperty); }
            set { SetValue(CurrentLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentLinkProperty =
            DependencyProperty.Register("CurrentLink", typeof(IAction), typeof(OfficeWindowBase), new PropertyMetadata(default(IAction), (d, e) =>
            {
                OfficeWindowBase control = d as OfficeWindowBase;

                if (control == null) return;

                IAction config = e.NewValue as IAction;

            }));


        /// <summary>
        /// 显示文件
        /// </summary>
        public bool IsShowLink
        {
            get { return (bool)GetValue(IsShowLinkProperty); }
            set { SetValue(IsShowLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowLinkProperty =
            DependencyProperty.Register("IsShowLink", typeof(bool), typeof(OfficeWindowBase), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 OfficeWindowBase control = d as OfficeWindowBase;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

                 //control.RefreshCaption();

                 //DynamicResourceExtension 
             }));

        //private void RefreshCaption()
        //{
        //    this.CaptionBackground = this.IsShowLink ? Application.Current.FindResource(BrushKeys.BackgroundDefault) as Brush : Application.Current.FindResource(BrushKeys.Accent) as Brush;
        //    this.CaptionForeground = this.IsShowLink ? Application.Current.FindResource(BrushKeys.ForegroundDefault) as Brush : Application.Current.FindResource(BrushKeys.ForegroundWhite) as Brush;

        //}


        public Style LeftMenuStyle
        {
            get { return (Style)GetValue(LeftMenuStyleProperty); }
            set { SetValue(LeftMenuStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftMenuStyleProperty =
            DependencyProperty.Register("LeftMenuStyle", typeof(Style), typeof(OfficeWindowBase), new FrameworkPropertyMetadata(default(Style), (d, e) =>
            {
                OfficeWindowBase control = d as OfficeWindowBase;

                if (control == null) return;

                if (e.OldValue is Style o)
                {

                }

                if (e.NewValue is Style n)
                {

                }

            }));

    }
}
