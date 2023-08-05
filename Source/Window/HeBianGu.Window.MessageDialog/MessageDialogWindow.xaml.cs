// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Window.MessageDialog
{
    /// <summary>
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageDialogWindow : IDialogWindow
    {
        public MessageDialogWindow()
        {
            InitializeComponent();
        }

        #region - 动态加载按钮 -

        /// <summary> 显示窗口 </summary>
        public static bool ShowDialog(string messge, string title = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action<IDialogWindow>>[] acts)
        {
            if (showEffect)
            {
                MessageProxy.Messager.BeginDefaultBlurEffect(true);

            }

            MessageDialogWindow m = new MessageDialogWindow();

            m.messageText.Text = messge;
            char[] array = messge?.ToArray();

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
                foreach (Tuple<string, Action<IDialogWindow>> item in acts)
                {
                    Button f = new Button();
                    f.Content = item.Item1;
                    f.Margin = new Thickness(0, 0, 0, 0);
                    f.Click += (object sender, RoutedEventArgs e) =>
                    {
                        if (item.Item2 != null)
                        {
                            item.Item2.Invoke(m);
                        }
                        m.CloseAnimation(m);
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
            if (showEffect)
            {
                MessageProxy.Messager.BeginDefaultBlurEffect(false);

            }
            return m.Result;
        }

        /// <summary> 显示窗口 </summary>
        public static int ShowDialogWith(string messge, string title = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts)
        {
            if (showEffect)
            {
                MessageProxy.Messager.BeginDefaultBlurEffect(true);

            }

            int result = -1;

            MessageDialogWindow m = new MessageDialogWindow();

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
                foreach (Tuple<string, Action<IDialogWindow>> item in acts)
                {
                    Button f = new Button();
                    f.Content = item.Item1;
                    //f.Width = double.NaN;
                    f.Margin = new Thickness(0, 0, 10, 0);
                    //f.Icon = "";
                    //f.SetPressed(false);

                    //f.Style = Application.Current.FindResource(ButtonKeys.Dynamic) as Style;

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

            if (showEffect)
            {
                MessageProxy.Messager.BeginDefaultBlurEffect(false);

            }


            return result;
        }

        public static bool ShowSumit(string messge, string title = null, bool showEffect = false, int closeTime = -1)
        {
            var act = new Tuple<string, Action<IDialogWindow>>("确定", x => x.Result = true);
            return ShowDialog(messge, title, closeTime, showEffect, act);
        }

        #endregion

        #region - 带蒙版的消息 -

        public static void ShowDialog(string message, Grid owner)
        {
            //蒙板
            Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) };

            //在上面放一层蒙板
            owner.Children.Add(layer);

            //弹出消息框
            MessageDialogWindow box = new MessageDialogWindow();
            box.Tag = owner;
            box.Title = message;
            box.Closed += Window_Closed;

            Button f = new Button();
            f.Content = "确定";
            f.IsDefault = true;
            f.Margin = new Thickness(0, 0, 20, 0);
            //f.Style = f.FindResource("S.Button.Default") as Style;

            //f.Style = myResourceDictionary["Button_LinkButton"] as Style;

            //f.Style = Application.Current.FindResource(ButtonKeys.Dynamic) as Style;

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
            MessageDialogWindow m = sender as MessageDialogWindow;

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
            this.Result = true;
            this.CloseAnimation(this);
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CloseAnimation(this);
        }
    }
}
