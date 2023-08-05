// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Systems.Setting
{
    /// <summary>
    /// SettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SettingDialog : UserControl
    {
        public SettingDialog()
        {
            InitializeComponent();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            MessageProxy.Container.Close();

            HeBianGu.Base.WpfBase.SystemSetting.Instance?.Cancel();
        }

        private void Button_Sumit(object sender, RoutedEventArgs e)
        {
            string message = null;
            bool? result = HeBianGu.Base.WpfBase.SystemSetting.Instance?.Save(out message);
            if (result == false)
            {
                MessageProxy.Snacker.ShowTime(message);
                return;
            }
            this.OnSumit();
            this.Result = true;
            MessageProxy.Container.Close();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent SumitRoutedEvent =
            EventManager.RegisterRoutedEvent("Sumit", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(SettingDialog));
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


        public bool Result { get; private set; }

        private void Button_SetDefault(object sender, RoutedEventArgs e)
        {
            HeBianGu.Base.WpfBase.SystemSetting.Instance?.SetDefault();
        }
    }


}
