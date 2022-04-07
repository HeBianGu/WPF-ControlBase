// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.General.WpfControlLib
{
    public class Dialog : ContentControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Dialog), "S.Dialog.Default");
        public static ComponentResourceKey DispalyKey => new ComponentResourceKey(typeof(Dialog), "S.Dialog.Dispaly");
        static Dialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog), new FrameworkPropertyMetadata(typeof(Dialog)));
        }

        public Dialog()
        {
            {
                CommandBinding binding = new CommandBinding(CommandService.Cancel);
                binding.Executed += (l, k) =>
                {
                    this.Result = false;
                    this.OnCancel();
                    Message.Instance.CloseLayer();
                };

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(CommandService.Sure);
                binding.Executed += (l, k) =>
                {
                    this.Result = true;
                    this.OnSumit();
                    Message.Instance.CloseLayer();
                };

                this.CommandBindings.Add(binding);
            }
        }


        public bool Result
        {
            get { return (bool)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ResultProperty =
            DependencyProperty.Register("Result", typeof(bool), typeof(Dialog), new FrameworkPropertyMetadata(default(bool), (d, e) =>
             {
                 Dialog control = d as Dialog;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        //声明和注册路由事件
        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Dialog));
        //CLR事件包装
        public event RoutedEventHandler Sumit
        {
            add { this.AddHandler(SumitRoutedEvent, value); }
            remove { this.RemoveHandler(SumitRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnSumit()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitRoutedEvent, this);
            this.RaiseEvent(args);
        }


        //声明和注册路由事件
        public static readonly RoutedEvent CancelRoutedEvent =
            EventManager.RegisterRoutedEvent("Cancel", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Dialog));
        //CLR事件包装
        public event RoutedEventHandler Cancel
        {
            add { this.AddHandler(CancelRoutedEvent, value); }
            remove { this.RemoveHandler(CancelRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnCancel()
        {
            RoutedEventArgs args = new RoutedEventArgs(CancelRoutedEvent, this);
            this.RaiseEvent(args);
        }
    }
}
