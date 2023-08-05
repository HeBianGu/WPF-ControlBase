using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Manager.View.Panel
{
    /// <summary>
    /// ContainControl.xaml 的交互逻辑
    /// </summary>
    public partial class ContainControl : UserControl
    {
        public ContainControl()
        {
            //InitializeComponent();
        }

        private readonly Random _rng = new Random();

        private void Button_CP_Click(object sender, RoutedEventArgs e)
        {
            ContentControl content = new ContentControl
            {
                Width = _rng.Next(100, (int)ActualWidth),
                Height = _rng.Next(100, (int)ActualHeight),

                Content = new ContainPanelSample()
            };

            cp.Add(content);
        }

        private void Button_CP_Click_Close(object sender, RoutedEventArgs e)
        {
            cp.Remove();
        }

    }

    internal class ContainPanelSample
    {

    }
}
