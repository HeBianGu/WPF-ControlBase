using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
