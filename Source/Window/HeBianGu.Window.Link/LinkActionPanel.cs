// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Panel;
using System.Windows;

namespace HeBianGu.Window.Link
{
    public class LinkActionPanel : ContainPanel
    {

        public IAction LinkAction
        {
            get { return (IAction)GetValue(LinkActionProperty); }
            set { SetValue(LinkActionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkActionProperty =
            DependencyProperty.Register("LinkAction", typeof(IAction), typeof(LinkActionPanel), new PropertyMetadata(default(IAction), (d, e) =>
             {
                 LinkActionPanel control = d as LinkActionPanel;

                 if (control == null) return;

                 control.Refresh(e.NewValue as IAction);

             }));

        private async void Refresh(IAction neww)
        {
            if (this.LinkAction == null) return;

            if (this.Children.Count > 0)
            {
                this.Remove(this.Children[0]);
            }

            IActionResult result = await this.LinkAction.GetActionResult();

            this.Add(result.View as UIElement);
        }

    }

}
