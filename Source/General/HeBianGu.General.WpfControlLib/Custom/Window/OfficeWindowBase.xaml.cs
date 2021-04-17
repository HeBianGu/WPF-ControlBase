using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public partial class OfficeWindowBase : MainWindowBase
    {
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
}
