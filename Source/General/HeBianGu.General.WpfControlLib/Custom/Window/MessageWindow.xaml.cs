using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow 
    {
        public MessageWindow()
        {
            InitializeComponent();
        }


        #region - 动态加载按钮 -

        private bool _result;
        /// <summary> 说明 </summary>
        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        /// <summary> 显示窗口 </summary>
        public static bool ShowDialog(string messge, string title = null, int closeTime = -1, bool isuseBlur = false, params Tuple<string, Action>[] acts)
        {
            if (isuseBlur)
            {
                MessageService.BeginDefaultBlurEffect(true);

            }

            MessageWindow m = new MessageWindow();

            m.messageText.Text = messge;
            var array = messge.ToArray();

            var c = array.ToList().Count(l => l == '\r');

            m.Height += c * 30;

            if (!string.IsNullOrEmpty(title))
            {
                m.Title = title;
            }

            if (acts == null || acts.Length == 0)
            {

            }
            else
            {
                m.actionPanel.Children.Clear();

                foreach (var item in acts)
                {
                    FButton f = new FButton();
                    f.Content = item.Item1;
                    f.Margin = new Thickness(0, 0, 0, 0);
                    //f.Style = f.FindResource("S.FButton.Default") as Style;
                    f.Style = Application.Current.FindResource("S.FButton.Style.Default") as Style;
                    f.Click += (object sender, RoutedEventArgs e) =>
                    {
                        m.CloseAnimation(m);

                        if (item.Item2 != null)
                        {
                            item.Item2.Invoke();
                        }
                    };

                    m.actionPanel.Children.Add(f);
                }
            }

            if (closeTime != -1)
            {
                Action action = () =>
                {
                    for (int i = closeTime; i > 0; i--)
                    {
                        Thread.Sleep(1000);

                        m.Dispatcher.Invoke(() => m.Title = title + " 关闭倒计时(" + i + ")秒");
                    }

                    m.Dispatcher.Invoke(() => m.CloseAnimation(m));
                };


                Task task = new Task(action);
                task.Start();
            }


            m.ShowDialog();

            if (isuseBlur)
            {
                MessageService.BeginDefaultBlurEffect(false);

            }


            return m._result;
        }

        /// <summary> 显示窗口 </summary>
        public static int ShowDialogWith(string messge, string title = null, bool isuseBlur = false, params Tuple<string, Action<MessageWindow>>[] acts)
        {
            if (isuseBlur)
            {
                MessageService.BeginDefaultBlurEffect(true);

            }

            int result = -1;

            MessageWindow m = new MessageWindow();

            // Todo ：消息 2017-07-28 10:46:24 
            m.messageText.Text = messge;

            var array = messge.ToArray();

            var c = array.ToList().Count(l => l == '\r');

            m.Height += c * 30;

            List<Label> ls = new List<Label>();

            if (!string.IsNullOrEmpty(title))
            {
                m.Title = title;
            }

            if (acts == null || acts.Length == 0)
            {

            }
            else
            {
                m.actionPanel.Children.Clear();

                // Todo ：根据事件加载按钮 2017-07-28 10:46:07
                foreach (var item in acts)
                {
                    FButton f = new FButton();
                    f.Content = item.Item1;
                    //f.Width = double.NaN;
                    f.Margin = new Thickness(0, 0, 10, 0);
                    //f.FIcon = "";
                    //f.SetPressed(false);

                    f.Style = Application.Current.FindResource("S.FButton.Style.Default") as Style;

                    f.Click += (object sender, RoutedEventArgs e) =>
                    {
                        if (item.Item2 != null)
                        {
                            item.Item2(m);
                        }

                        result = acts.ToList().IndexOf(item);

                        m.CloseAnimation(m);
                    };

                    m.actionPanel.Children.Add(f);
                }
            }


            m.ShowDialog();

            if (isuseBlur)
            {
                MessageService.BeginDefaultBlurEffect(false);

            }


            return result;
        }

        /// <summary> 只有确定的按钮 </summary>
        public static bool ShowSumit(string messge, string title = null, int closeTime = -1)
        {
            Tuple<string, Action> act = new Tuple<string, Action>("确定", null);

            ShowDialog(messge, title, closeTime, false, act);

            return true;
        }

        #endregion

        #region - 带蒙版的消息 -

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="owner">父级窗体</param>
        public static void ShowDialog(string message, Grid owner)
        {
            //蒙板
            Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) };

            //在上面放一层蒙板
            owner.Children.Add(layer);

            //弹出消息框
            MessageWindow box = new MessageWindow();
            box.Tag = owner;
            box.Title = message;
            box.Closed += Window_Closed;

            FButton f = new FButton();
            f.Content = "确定";
            f.Margin = new Thickness(0, 0, 20, 0);
            //f.Style = f.FindResource("S.FButton.Default") as Style;

            //f.Style = myResourceDictionary["FButton_LinkButton"] as Style;

            f.Style = Application.Current.FindResource("S.FButton.Style.Default") as Style;

            f.Click += (object sender, RoutedEventArgs e) =>
            {
                box.CloseAnimation(box);
            };

            box.actionPanel.Children.Add(f);

            box.ShowDialog();
        }

        /// <summary>  窗体关闭事件  </summary>
        private static void Window_Closed(object sender, EventArgs e)
        {
            MessageWindow m = sender as MessageWindow;

            if (m.Tag == null) return;

            //容器Grid
            Grid grid = m.Tag as Grid;

            if (grid == null) return;

            //父级窗体原来的内容
            UIElement original = VisualTreeHelper.GetChild(grid, VisualTreeHelper.GetChildrenCount(grid) - 1) as UIElement;

            //将父级窗体原来的内容在容器Grid中移除
            grid.Children.Remove(original);
            //赋给父级窗体
            m.Tag = original;
        }

        #endregion


        /// <summary> 确定 </summary>
        private void sumitBtn_Click(object sender, RoutedEventArgs e)
        {
            _result = true;
            this.CloseAnimation(this);
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CloseAnimation(this);
        }
    }
}
