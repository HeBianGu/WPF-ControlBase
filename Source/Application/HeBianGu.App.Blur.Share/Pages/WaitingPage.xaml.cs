using System.Windows;
using System.Windows.Controls;

namespace WpfControlDemo.View
{
    /// <summary>
    /// WaitingPage.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingPage : Page
    {
        //private IAsynNotify Asyn;

        public WaitingPage()
        {

            InitializeComponent();

            //this.Asyn = new DefaultAsynNotify();
            //this.pro1.DataContext = this.Asyn;
            //this.pro2.DataContext = this.Asyn;

            //this.pro3.DataContext = this.Asyn;

            //this.pro4.DataContext = this.Asyn;
            //this.pro5.DataContext = this.Asyn;
            //this.pro6.DataContext = this.Asyn;
            //this.proValue.DataContext = this.Asyn;
        }

        //private void btn_success_Click(object sender, RoutedEventArgs e)
        //{
        //    Action action = () =>
        //    {
        //        for (int i = 0; i < 100; i += 1)
        //        {
        //            this.Asyn.Advance(1);
        //            System.Threading.Thread.Sleep(50);
        //        }
        //        this.Asyn.IsSuccess = true;
        //    };

        //    this.Asyn.Start(100);

        //    action.DoTask();


        //}

        //private void btn_faled_Click(object sender, RoutedEventArgs e)
        //{

        //    Action action = () =>
        //      {
        //          for (int i = 0; i < 100; i++)
        //          {
        //              this.Asyn.Advance(1);
        //              System.Threading.Thread.Sleep(50);
        //              if (i >= 30)
        //              {
        //                  this.Asyn.Cancel();
        //                  this.Asyn.IsSuccess = false;
        //                  break;
        //              }
        //          }
        //      };

        //    this.Asyn.Start(100);
        //    action.DoTask();
        //}

        private void btn_repeat_Click(object sender, RoutedEventArgs e)
        {
            //this.Asyn.Start(0);
        }

        private void btn_showwindow_Click(object sender, RoutedEventArgs e)
        {
            //string format = "正在处理{0}/{1}";

            //bool isBreak = false;

            //Action<ProgressWindow> action = l =>
            //{
            //    for (int i = 1; i < 19; i++)
            //    {
            //        if (isBreak) break;

            //        Thread.Sleep(1000);

            //        l.SetPercent(18, i, string.Format(format, i, 18));
            //    }
            //};

            //Action cancelAction = () => isBreak = true;

            //Tuple<string, Action> cancel = new Tuple<string, Action>("取消", cancelAction);

            //ProgressWindow.ShowDialogWith(action, "正在上传", cancel);
        }
    }
}
