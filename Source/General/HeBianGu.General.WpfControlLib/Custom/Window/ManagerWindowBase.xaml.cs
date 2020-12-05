using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary> 链接主窗口 </summary>
    [TemplatePart(Name = "PART_TITLEGRID", Type = typeof(Grid))]
    public partial class ManagerWindowBase : MainWindowBase
    {
        Grid _titleGrid = null;

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this._titleGrid = Template.FindName("PART_TITLEGRID", this) as Grid;

            if (this._titleGrid != null)
            {
                this._titleGrid.MouseDown += (l, k) =>
                {
                    try
                    {
                        this.DragMove();
                    }
                    catch
                    {

                    }

                };
            }
        }

        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(ManagerWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        public ILinkActionBase CurrentLink
        {
            get { return (ILinkActionBase)GetValue(CurrentLinkProperty); }
            set { SetValue(CurrentLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentLinkProperty =
            DependencyProperty.Register("CurrentLink", typeof(ILinkActionBase), typeof(ManagerWindowBase), new PropertyMetadata(default(ILinkActionBase), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                ILinkActionBase config = e.NewValue as ILinkActionBase;

            }));


        public object CustomContent
        {
            get { return (object)GetValue(CustomContentProperty); }
            set { SetValue(CustomContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomContentProperty =
            DependencyProperty.Register("CustomContent", typeof(object), typeof(ManagerWindowBase), new PropertyMetadata(default(object), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                object config = e.NewValue as object;

            }));
    }

    /// <summary> 链接主窗口 </summary>
    public partial class LinkGroupsManagerWindowBase : ManagerWindowBase
    {
        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();
        }

        public LinkGroupsManagerWindowBase()
        {
            this.Loaded += (l, k) =>
            {
                var find = this.LinkActionGroups.FirstOrDefault(f => f.IsExpanded)?? this.LinkActionGroups.FirstOrDefault();

                this.CurrentLink = find?.Links?.FirstOrDefault();
            };
        }


        public LinkActionGroups LinkActionGroups
        {
            get { return (LinkActionGroups)GetValue(LinkActionGroupsProperty); }
            set { SetValue(LinkActionGroupsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionGroupsProperty =
            DependencyProperty.Register("LinkActionGroups", typeof(LinkActionGroups), typeof(ManagerWindowBase), new PropertyMetadata(new LinkActionGroups(true), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                LinkActionGroups config = e.NewValue as LinkActionGroups;

                if (config == null) return;

            }));
    }

    /// <summary> 链接主窗口 </summary>
    public partial class LinkActionsManagerWindowBase : ManagerWindowBase
    {
        public LinkActionsManagerWindowBase()
        {
            this.Loaded += (l, k) =>
            {
                this.CurrentLink = this.LinkActions?.FirstOrDefault();
            };
        }

        public ObservableCollection<ILinkActionBase> LinkActions
        {
            get { return (ObservableCollection<ILinkActionBase>)GetValue(LinkActionsProperty); }
            set { SetValue(LinkActionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionsProperty =
            DependencyProperty.Register("LinkActions", typeof(ObservableCollection<ILinkActionBase>), typeof(LinkActionsManagerWindowBase), new PropertyMetadata(new ObservableCollection<ILinkActionBase>(), (d, e) =>
             {
                 LinkActionsManagerWindowBase control = d as LinkActionsManagerWindowBase;

                 if (control == null) return;

                 ObservableCollection<ILinkActionBase> config = e.NewValue as ObservableCollection<ILinkActionBase>;

             }));
    }


    /// <summary> 链接主窗口 </summary>
    public partial class CustomManagerWindowBase : ManagerWindowBase
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        } 

        public object LeftContent
        {
            get { return (object)GetValue(LeftContentProperty); }
            set { SetValue(LeftContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftContentProperty =
            DependencyProperty.Register("LeftContent", typeof(object), typeof(ManagerWindowBase), new PropertyMetadata(default(object), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                object config = e.NewValue as object;

            }));

    }
}
