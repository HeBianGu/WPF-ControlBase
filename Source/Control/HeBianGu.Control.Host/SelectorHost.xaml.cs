// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HeBianGu.Control.Host
{
    public class SelectorHost : ItemsHost
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(SelectorHost), "S.SelectorHost.Default");

        static SelectorHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectorHost), new FrameworkPropertyMetadata(typeof(SelectorHost)));
        }

        public SelectorHost()
        {
            this.Loaded += (l, k) =>
              {
                  Selector selector = this.GetChild<Selector>();
                  if (selector == null) return;
                  selector.SelectionChanged += Selector_SelectionChanged;

                  if (selector.SelectedItem == null) return;
                  UIElement element = selector.ItemContainerGenerator.ContainerFromItem(selector.SelectedItem) as UIElement;
                  if (element == null) return;
                  this.Location(element);
              };
        }

        private void Selector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = (Selector)sender;
            if (selector.SelectedItem == null) return;
            UIElement element = selector.ItemContainerGenerator.ContainerFromItem(selector.SelectedItem) as UIElement;
            if (element == null) return;
            this.Location(element);
        }



    }
}
