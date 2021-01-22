using HeBianGu.Base.WpfBase;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public ManagerWindowBase()
        {
            this.Loaded += (l, k) =>
              {
                  OnLoaded();
              };

        }

        protected virtual void OnLoaded()
        {

        }
    }

    /// <summary> 链接主窗口 </summary>
    public partial class LinkGroupsManagerWindowBase : ManagerWindowBase
    {
        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();
        }

        //public LinkGroupsManagerWindowBase()
        //{
        //    this.Loaded += (l, k) =>
        //    {
        //        var find = this.LinkActionGroups.FirstOrDefault(f => f.IsExpanded)?? this.LinkActionGroups.FirstOrDefault();

        //        this.CurrentLink = find?.Links?.FirstOrDefault();
        //    };
        //}

        protected override void OnLoaded()
        {
            base.OnLoaded();

            var find = this.LinkActionGroups.FirstOrDefault(f => f.IsExpanded) ?? this.LinkActionGroups.FirstOrDefault();

            this.CurrentLink = find?.Links?.FirstOrDefault();
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

            this.CurrentLink = this.LinkActions?.FirstOrDefault();
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

    /// <summary> 环绕操作窗口 </summary>
    public partial class CrossManagerWindowBase : LinkActionsManagerWindowBase
    {
        //protected override void OnLoaded()
        //{
        //    if(LoadDefault)
        //    {
        //        base.OnLoaded();
        //    }
        //}

        //public bool LoadDefault { get; set; }

        /// <summary> 环绕的布局容器 </summary>
        public ItemsPanelTemplate ItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplate), typeof(CrossManagerWindowBase), new PropertyMetadata(default(ItemsPanelTemplate), (d, e) =>
             {
                 CrossManagerWindowBase control = d as CrossManagerWindowBase;

                 if (control == null) return;

                 ItemsPanelTemplate config = e.NewValue as ItemsPanelTemplate;

             }));


        public DataTemplate LinkActionDataTemplate
        {
            get { return (DataTemplate)GetValue(LinkActionDataTemplateProperty); }
            set { SetValue(LinkActionDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionDataTemplateProperty =
            DependencyProperty.Register("LinkActionDataTemplate", typeof(DataTemplate), typeof(CrossManagerWindowBase), new PropertyMetadata(default(DataTemplate), (d, e) =>
             {
                 CrossManagerWindowBase control = d as CrossManagerWindowBase;

                 if (control == null) return;

                 DataTemplate config = e.NewValue as DataTemplate;

             }));


        public Geometry ContenClip
        {
            get { return (Geometry)GetValue(ContenClipProperty); }
            set { SetValue(ContenClipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContenClipProperty =
            DependencyProperty.Register("ContenClip", typeof(Geometry), typeof(CrossManagerWindowBase), new PropertyMetadata(default(Geometry), (d, e) =>
             {
                 CrossManagerWindowBase control = d as CrossManagerWindowBase;

                 if (control == null) return;

                 Geometry config = e.NewValue as Geometry;

             }));


    }

    /// <summary> 遮盖窗口 </summary>
    public partial class AboveManagerWindowBase : LinkGroupsManagerWindowBase
    {

        /// <summary> 环绕的布局容器 </summary>
        public ItemsPanelTemplate ItemsPanel
        {
            get { return (ItemsPanelTemplate)GetValue(ItemsPanelProperty); }
            set { SetValue(ItemsPanelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsPanelProperty =
            DependencyProperty.Register("ItemsPanel", typeof(ItemsPanelTemplate), typeof(AboveManagerWindowBase), new PropertyMetadata(default(ItemsPanelTemplate), (d, e) =>
            {
                AboveManagerWindowBase control = d as AboveManagerWindowBase;

                if (control == null) return;

                ItemsPanelTemplate config = e.NewValue as ItemsPanelTemplate;

            }));


        public DataTemplate LinkActionDataTemplate
        {
            get { return (DataTemplate)GetValue(LinkActionDataTemplateProperty); }
            set { SetValue(LinkActionDataTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionDataTemplateProperty =
            DependencyProperty.Register("LinkActionDataTemplate", typeof(DataTemplate), typeof(AboveManagerWindowBase), new PropertyMetadata(default(DataTemplate), (d, e) =>
            {
                AboveManagerWindowBase control = d as AboveManagerWindowBase;

                if (control == null) return;

                DataTemplate config = e.NewValue as DataTemplate;

            }));


        public bool IsboveState
        {
            get { return (bool)GetValue(IsboveStateProperty); }
            set { SetValue(IsboveStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsboveStateProperty =
            DependencyProperty.Register("IsboveState", typeof(bool), typeof(AboveManagerWindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 AboveManagerWindowBase control = d as AboveManagerWindowBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        protected override void OnLoaded()
        {
            return;
        }
    }
}
