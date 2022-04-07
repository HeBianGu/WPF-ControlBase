using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.App.Touch.View.Share
{
    /// <summary>
    /// KeyBoardControl.xaml 的交互逻辑
    /// </summary>
    public partial class KeyBoardControl : UserControl
    {
        public KeyBoardControl()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button == null) return;

            Debug.WriteLine(button.Content);

            string content = button.Content.ToString();

            string tag = button.Tag.ToString();

            byte b = Convert.ToByte(tag);

            KeyHelper.OnKeyPress(b);
        }
    }
}
