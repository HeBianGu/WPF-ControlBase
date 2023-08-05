// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
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

namespace HeBianGu.Window.MessageDialog
{
    public partial class ObjectDialogWindow : IDialogWindow
    {
        public ObjectDialogWindow()
        {
            InitializeComponent();
            {
                CommandBinding binding = new CommandBinding(Commander.Sure);
                binding.Executed += (l, k) =>
                {
                    this.Result = true;
                    this.CloseAnimation(this);
                };
                this.CommandBindings.Add(binding);
            }
            {
                CommandBinding binding = new CommandBinding(Commander.Cancel);
                binding.Executed += (l, k) =>
                {
                    this.CloseAnimation(this);
                };
                this.CommandBindings.Add(binding);
            }

        }

        #region - 动态加载按钮 -

        /// <summary> 显示窗口 </summary>
        public static bool ShowDialog(object content, string title = null, Action<System.Windows.Window> builder = null, int closeTime = -1, bool showEffect = true, params Tuple<string, Action>[] acts)
        {
            if (showEffect)
            {
                MessageProxy.Messager.BeginDefaultBlurEffect(true);
            }
            ObjectDialogWindow m = new ObjectDialogWindow();
            builder?.Invoke(m);
            m.contentControl.Content = content;
            if (!string.IsNullOrEmpty(title))
            {
                m.Title = title;
            }

            if (acts != null)
            {
                m.actionPanel.Children.Clear();
                foreach (Tuple<string, Action> item in acts)
                {
                    Button f = new Button();
                    f.Content = item.Item1;
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

                if (m.actionPanel.Children.Count == 0)
                {
                    m.actionPanel.Visibility = Visibility.Collapsed;
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
        public static int ShowDialogWith(object content, string title = null, Action<System.Windows.Window> builder = null, bool showEffect = false, params Tuple<string, Action<IDialogWindow>>[] acts)
        {
            if (showEffect)
            {
                MessageProxy.Messager.BeginDefaultBlurEffect(true);
            }
            int result = -1;
            ObjectDialogWindow m = new ObjectDialogWindow();
            builder?.Invoke(m);
            m.contentControl.Content = content;
            List<Label> ls = new List<Label>();
            if (!string.IsNullOrEmpty(title))
            {
                m.Title = title;
            }

            if (acts != null)
            {
                m.actionPanel.Children.Clear();
                foreach (Tuple<string, Action<IDialogWindow>> item in acts)
                {
                    Button f = new Button();
                    f.Content = item.Item1;
                    f.Margin = new Thickness(0, 0, 10, 0);
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
                if (m.actionPanel.Children.Count == 0)
                {
                    m.actionPanel.Visibility = Visibility.Collapsed;
                }
            }

            m.ShowDialog();

            if (showEffect)
            {
                MessageProxy.Messager.BeginDefaultBlurEffect(false);
            }
            return result;
        }

        /// <summary> 只有确定的按钮 </summary>
        public static bool ShowSumit(object messge, string title = null, Action<System.Windows.Window> builder = null, bool showEffect = false, int closeTime = -1)
        {
            Tuple<string, Action> act = new Tuple<string, Action>("确定", null);
            ShowDialog(messge, title, builder, closeTime, showEffect, act);
            return true;
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
