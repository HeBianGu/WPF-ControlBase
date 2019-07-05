using HeBianGu.Base.WpfBase;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{

    public class LinkGroupExpander : ItemsControl, ICommandSource
    {

        //static LinkGroupExpander()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkGroupExpander), new FrameworkPropertyMetadata(typeof(LinkGroupExpander)));
        //} 


        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        public LinkAction SelectedLink
        {
            get { return (LinkAction)GetValue(SelectedLinkProperty); }
            set { SetValue(SelectedLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLinkProperty =
            DependencyProperty.Register("SelectedLink", typeof(LinkAction), typeof(LinkGroupExpander), new PropertyMetadata(default(LinkAction), (d, e) =>
             {
                 LinkGroupExpander control = d as LinkGroupExpander;
                 if (control == null) return;

                 if(e.NewValue ==null)
                 {
                     control.SelectedLink = e.OldValue as LinkAction;
                 }

                 control.Command?.Execute(control.CommandParameter);

             }));

    }

}
