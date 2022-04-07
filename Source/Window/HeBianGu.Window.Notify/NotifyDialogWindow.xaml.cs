// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Window.Notify
{
    public partial class NotifyDialogWindow : NotifyWindowBase
    {
        public NotifyDialogWindow()
        {
            InitializeComponent();
        }

        #region - 动态加载按钮 -

        private bool _result;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public static void ShowMessage(string messge, string title = null, int closeTime = -1)
        {
            NotifyDialogWindow m = new NotifyDialogWindow();

            m.messageText.Text = messge;

            m.actionPanel.Visibility = Visibility.Collapsed;

            char[] array = messge.ToArray();

            int c = array.ToList().Count(l => l == '\r');

            m.Height += c * 30;

            if (!string.IsNullOrEmpty(title))
            {
                m.Title = title;
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

            m.Show();
        }

        public static bool ShowDialog(string messge, string title = null, int closeTime = -1, params Tuple<string, Action>[] acts)
        {
            NotifyDialogWindow m = new NotifyDialogWindow();

            m.messageText.Text = messge;
            char[] array = messge.ToArray();

            int c = array.ToList().Count(l => l == '\r');

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

                foreach (Tuple<string, Action> item in acts)
                {
                    Button f = new Button();
                    f.Content = item.Item1;
                    f.Margin = new Thickness(0, 0, 0, 0);
                    //f.Style = f.FindResource("S.Button.Default") as Style;

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





            return m.Result;
        }

        public static int ShowDialogWith(string messge, string title = null, params Tuple<string, Action<NotifyDialogWindow>>[] acts)
        {
            int result = -1;

            NotifyDialogWindow m = new NotifyDialogWindow();

            // Todo ：消息 2017-07-28 10:46:24 
            m.messageText.Text = messge;

            char[] array = messge.ToArray();

            int c = array.ToList().Count(l => l == '\r');

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
                foreach (Tuple<string, Action<NotifyDialogWindow>> item in acts)
                {
                    Button f = new Button();
                    f.Content = item.Item1;
                    f.Margin = new Thickness(0, 0, 10, 0);
                    //f.Icon = "";
                    //f.SetPressed(false);

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


            return result;
        }

        public static bool ShowSumit(string messge, string title = null, int closeTime = -1)
        {
            Tuple<string, Action> act = new Tuple<string, Action>("确定", null);

            ShowDialog(messge, title, closeTime, act);

            return true;
        }

        #endregion

        #region - 带蒙版的消息 -

        public static void ShowDialog(string message, Grid owner)
        {
            Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) };

            owner.Children.Add(layer);

            NotifyDialogWindow box = new NotifyDialogWindow();
            box.Tag = owner;
            box.Title = message;
            box.Closed += Window_Closed;

            Button f = new Button();
            f.Content = "确定";
            f.Margin = new Thickness(0, 0, 20, 0);
            //f.Style = f.FindResource("S.Button.Default") as Style;

            //f.Style = myResourceDictionary["Button_LinkButton"] as Style;

            f.Click += (object sender, RoutedEventArgs e) =>
            {
                box.CloseAnimation(box);
            };

            box.actionPanel.Children.Add(f);

            box.ShowDialog();
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            NotifyDialogWindow m = sender as NotifyDialogWindow;

            if (m.Tag == null) return;

            Grid grid = m.Tag as Grid;

            if (grid == null) return;

            UIElement original = VisualTreeHelper.GetChild(grid, VisualTreeHelper.GetChildrenCount(grid) - 1) as UIElement;

            grid.Children.Remove(original);
            m.Tag = original;
        }

        #endregion

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
