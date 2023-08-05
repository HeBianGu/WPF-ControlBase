using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfControlDemo.View
{
    /// <summary>
    /// NotifyPage.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyPage : Page
    {
        private readonly Storyboard iconAnimation;

        public NotifyPage()
        {
            InitializeComponent();

            iconAnimation = Resources["IconAnimation"] as Storyboard;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            notifyIcon.ShowBalloonTip(1000, "Balloon", "Balloon tip demo.", NotifyBalloonIcon.Info);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            iconAnimation.Begin(this, true);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            iconAnimation.Stop(this);
        }
    }
}
