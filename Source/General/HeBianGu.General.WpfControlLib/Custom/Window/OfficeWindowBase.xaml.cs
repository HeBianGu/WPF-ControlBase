using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public partial class OfficeWindowBase : MainWindowBase
    {
        public OfficeWindowBase()
        {
            this.Loaded +=(l, k) =>
             {
                 this.CurrentLink = this.LinkActions?.FirstOrDefault();
             };

        }
        public object ToolContent
        {
            get { return (object)GetValue(ToolContentProperty); }
            set { SetValue(ToolContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolContentProperty =
            DependencyProperty.Register("ToolContent", typeof(object), typeof(OfficeWindowBase), new PropertyMetadata(default(object), (d, e) =>
             {
                 OfficeWindowBase control = d as OfficeWindowBase;

                 if (control == null) return;

                 object config = e.NewValue as object;

             })); 

        public object TitleContent
        {
            get { return (object)GetValue(TitleContentProperty); }
            set { SetValue(TitleContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleContentProperty =
            DependencyProperty.Register("TitleContent", typeof(object), typeof(OfficeWindowBase), new PropertyMetadata(default(object), (d, e) =>
             {
                 OfficeWindowBase control = d as OfficeWindowBase;

                 if (control == null) return;

                 object config = e.NewValue as object;

             }));




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

        public ILinkActionBase CurrentLink
        {
            get { return (ILinkActionBase)GetValue(CurrentLinkProperty); }
            set { SetValue(CurrentLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentLinkProperty =
            DependencyProperty.Register("CurrentLink", typeof(ILinkActionBase), typeof(OfficeWindowBase), new PropertyMetadata(default(ILinkActionBase), (d, e) =>
            {
                OfficeWindowBase control = d as OfficeWindowBase;

                if (control == null) return;

                ILinkActionBase config = e.NewValue as ILinkActionBase;

            }));

    }
}
