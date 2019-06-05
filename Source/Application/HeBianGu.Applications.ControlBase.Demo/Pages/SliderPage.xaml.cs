using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlDemo.View
{
    /// <summary>
    /// SliderPage.xaml 的交互逻辑
    /// </summary>
    public partial class SliderPage : Page
    {

        MyClass _vm = new MyClass();

        public SliderPage()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                this.DataContext = _vm;
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<IBufferPlayEntity> bufferPlays = new List<IBufferPlayEntity>();

            //  Message：构造1000个测试数据
            for (int i = 0; i < 1000; i++)
            {
                BufferPlayEntity entity = new BufferPlayEntity();

                bufferPlays.Add(entity);
            }

            //  Message：初始化控件
            this.control_bufferPlay.MinValue = 0;
            this.control_bufferPlay.Value = 0;
            this.control_bufferPlay.BufferValue = 0;
            this.control_bufferPlay.MaxValue = bufferPlays.Count;

            //  Message：开始缓冲引擎
            BufferPlayEngine bufferPlayEngine = new BufferPlayEngine(bufferPlays);

            bufferPlayEngine.RefreshCapacity(5);

            bufferPlayEngine.Start();

            Action<bool, int, int> action = (l, k, n) =>
              {
                  Application.Current.Dispatcher.Invoke(() =>
                  {
                      if (l)
                      {
                          this.txt_persent.Text = "缓冲完成..";
                      }
                      else
                      {
                          string p = (Convert.ToDouble(k) * 100 / Convert.ToDouble(n)).ToString();

                          this.txt_persent.Text = "缓冲中.." + p + "%";
                      }
                  });

              };

            //  Message：刷新播放进度
            Task.Run(() =>
            {
                for (int i = 0; i < bufferPlays.Count; i++)
                {
                    //  Message：设置当前播放进度值
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.control_bufferPlay.Value = i;
                    });

                    //  Message：检查当前是否已经暂停
                    while (true)
                    {

                        bool result = false;

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            result = this.btn_play.Content.ToString() == "暂停";
                        });

                        if (result) break;

                        Thread.Sleep(1000);
                    }

                    Thread.Sleep(100);

                    //  Message：阻塞等待当前进度是否可以播放
                    bufferPlayEngine.GetWaitCurrent(l => l == bufferPlays[i], action);

                }

            });

            //  Message：刷新下载进度
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(100);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.control_bufferPlay.BufferValue = bufferPlayEngine.GetBufferSize((int)this.control_bufferPlay.Value);
                    });

                }
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            button.Content = button.Content.ToString() == "暂停" ? "继续" : "暂停";
        }
    }

    public class BufferPlayEntity : BufferPlayEntityBase
    {
        public int IsLoaded { get; set; }

        Random random = new Random();

        public override void DoStart()
        {
            Thread.Sleep(random.Next(1, 2) * 1000);
        }
    }

    /// <summary> 说明 </summary>
    partial class MyClass
    {
        private double _left = 0.1;
        /// <summary> 说明 </summary>
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                RaisePropertyChanged();
            }
        }

        private double _right = 0.7;
        /// <summary> 说明 </summary>
        public double Right
        {
            get { return _right; }
            set
            {
                _right = value;
                RaisePropertyChanged();
            }
        }


    }

    partial class MyClass : INotifyPropertyChanged
    {
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
