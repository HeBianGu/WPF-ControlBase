using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfControlDemo.View
{
    /// <summary>
    /// TextBlockPage.xaml 的交互逻辑
    /// </summary>
    public partial class TextBlockPage : Page
    {
        public TextBlockPage()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (AnimatedTextBlock != null)
            {
                if (((CheckBox)sender).IsChecked == true)
                {
                    AnimatedTextBlock.RepeatBehavior = RepeatBehavior.Forever;
                }
                else
                {
                    AnimatedTextBlock.ClearValue(HeBianGu.General.WpfControlLib.AnimatedTextBlock.RepeatBehaviorProperty);
                }
            }
        }
    }
}
