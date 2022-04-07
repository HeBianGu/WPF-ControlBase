using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfControlDemo.View
{
    /// <summary>
    /// InlineDialogPage.xaml 的交互逻辑
    /// </summary>
    public partial class InlineDialogPage : Page
    {
        public InlineDialogPage()
        {
            InitializeComponent();
        }
        private readonly Random _rng = new Random();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InlineModalDialog dialog = new InlineModalDialog
            {
                Owner = this,
                Content = new DialogSampleContent(),
                Width = _rng.Next(100, (int)ActualWidth),
                Height = _rng.Next(100, (int)ActualHeight),
                ShowBlurrer = true
            };
            dialog.InputBindings.Add(new KeyBinding { Key = Key.Escape, Command = InlineModalDialog.CloseCommand });
            dialog.Show();
        }

        private void Button_CP_Click(object sender, RoutedEventArgs e)
        {
            ContentControl content = new ContentControl
            {
                Width = _rng.Next(100, (int)ActualWidth),
                Height = _rng.Next(100, (int)ActualHeight),


                Content = new ContainPanelSampleContent()
            };

            cp.Add(content);
        }

        private void Button_CP_Click_Close(object sender, RoutedEventArgs e)
        {
            cp.Remove();
        }
    }

    internal class DialogSampleContent
    {

    }

    internal class ContainPanelSampleContent
    {

    }
}
