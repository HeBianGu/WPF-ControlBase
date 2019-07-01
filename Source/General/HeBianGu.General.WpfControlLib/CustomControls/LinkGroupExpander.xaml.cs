using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{

    public class LinkGroupExpander : ListBox, ICommandSource
    {

        //static LinkGroupExpander()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(LinkGroupExpander), new FrameworkPropertyMetadata(typeof(LinkGroupExpander)));
        //} 

 
        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public IInputElement CommandTarget { get; set; }

        public Link SelectedLink
        {
            get { return (Link)GetValue(SelectedLinkProperty); }
            set { SetValue(SelectedLinkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedLinkProperty =
            DependencyProperty.Register("SelectedLink", typeof(Link), typeof(LinkGroupExpander), new PropertyMetadata(default(Link), (d, e) =>
             {
                 LinkGroupExpander control = d as LinkGroupExpander;

                 if (control == null) return;

                 Link config = e.NewValue as Link;

                 control.Command?.Execute(control.CommandParameter);

             }));

    }

}
