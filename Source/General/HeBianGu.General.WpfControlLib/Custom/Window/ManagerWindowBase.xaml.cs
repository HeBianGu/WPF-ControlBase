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

        public ObservableCollection<LinkActionGroup> LinkActionGroups
        {
            get { return (ObservableCollection<LinkActionGroup>)GetValue(LinkActionGroupsProperty); }
            set { SetValue(LinkActionGroupsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionGroupsProperty =
            DependencyProperty.Register("LinkActionGroups", typeof(ObservableCollection<LinkActionGroup>), typeof(ManagerWindowBase), new PropertyMetadata(new ObservableCollection<LinkActionGroup>(), (d, e) =>
            {
                ManagerWindowBase control = d as ManagerWindowBase;

                if (control == null) return;

                ObservableCollection<LinkActionGroup> config = e.NewValue as ObservableCollection<LinkActionGroup>;

            }));

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
    }


}
