using HeBianGu.Base.WpfBase;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Demo.Demo2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;

            //StoryboardSetting
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);

                    StyleSetting.Instance.StyleType = Base.WpfBase.StyleType.Reverse;

                    Thread.Sleep(2000);

                    StyleSetting.Instance.StyleType = Base.WpfBase.StyleType.Accent;

                    Thread.Sleep(2000);

                    StyleSetting.Instance.StyleType = Base.WpfBase.StyleType.Single;

                    Thread.Sleep(2000);

                    StyleSetting.Instance.StyleType = Base.WpfBase.StyleType.Default;

                    Thread.Sleep(2000);

                    StyleSetting.Instance.StyleType = Base.WpfBase.StyleType.Clear;

                }
            });


            //StyleSetting.Instance.MouseOverEffect = EffectType.Default2;

            //StyleSetting.Instance.SelectEffect = EffectType.Default4;
        }
    }

}
