// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
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
                CommandBinding binding = new CommandBinding(Commander.Cancel);
                binding.Executed += (l, k) =>
                {
                    this.Result = false;
                    this.OnCancel();
                    MessageProxy.Container.Close();
                };

                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(Commander.Sure);
                binding.Executed += (l, k) =>
                {
                    this.Result = true;
                    this.OnSumit();
                    MessageProxy.Container.Close();
                };

                this.CommandBindings.Add(binding);
            }
        }

        public bool Result
        {
            get { return (bool)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

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


        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Dialog));
        public event RoutedEventHandler Sumit
        {
            add { this.AddHandler(SumitRoutedEvent, value); }
            remove { this.RemoveHandler(SumitRoutedEvent, value); }
        }
        protected void OnSumit()
        {
            RoutedEventArgs args = new RoutedEventArgs(SumitRoutedEvent, this);
            this.RaiseEvent(args);
        }


        public static readonly RoutedEvent CancelRoutedEvent =
            EventManager.RegisterRoutedEvent("Cancel", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(Dialog));
        public event RoutedEventHandler Cancel
        {
            add { this.AddHandler(CancelRoutedEvent, value); }
            remove { this.RemoveHandler(CancelRoutedEvent, value); }
        }
        protected void OnCancel()
        {
            RoutedEventArgs args = new RoutedEventArgs(CancelRoutedEvent, this);
            this.RaiseEvent(args);
        }
    }
}
