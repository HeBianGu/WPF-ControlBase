// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.MessageContainer
{
    public abstract class MessageBase : ContentControl, INotifyMessageItem
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MessageBase), "S.MessageBase.Default");


        static MessageBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBase), new FrameworkPropertyMetadata(typeof(MessageBase)));
        }

        public static RoutedUICommand CloseCommand = new RoutedUICommand() { Text = "关闭" };

        public MessageBase()
        {
            {
                CommandBinding binding = new CommandBinding(CloseCommand, (l, k) =>
                {
                    //  Do ：删除消息
                    this.Close();
                });

                this.CommandBindings.Add(binding);

                this.Loaded += (l, k) =>
              {
                  TransitionService.SetIsVisible(this, true);
              };

            }
        }
        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(string), typeof(MessageBase), new PropertyMetadata(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (d, e) =>
             {
                 MessageBase control = d as MessageBase;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set
            {
                this.Dispatcher?.Invoke(() =>
                {
                    SetValue(MessageProperty, value);
                });
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageBase), new PropertyMetadata(default(string), (d, e) =>
             {
                 MessageBase control = d as MessageBase;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

        public void Close()
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageContainer parent = this.GetParent<MessageContainer>();

                if (parent == null) return;

                //this.BeginAnimationOpacity(1.0, 0.0, 500.0);

                //this.BeginAnimationX(0.0, 300.0, 500.0, l => parent.Items.Remove(this));

                TransitionService.SetIsVisible(this, false);

                if (TransitionService.GetVisibleActions(this)[0] is TransitionBase transitionBase)
                {
                    this.BeginAnimationOpacity(1.0, 0.0, transitionBase.HideDuration, l => parent.Items.Remove(this));
                }
            });
        }
    }
}
