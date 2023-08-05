// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace HeBianGu.Systems.Upgrade
{
    /// <summary>
    /// UpgradeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpgradeWindow
    {
        public UpgradeWindow()
        {
            InitializeComponent();
        }

        public List<string> Message
        {
            get { return (List<string>)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(List<string>), typeof(UpgradeWindow), new PropertyMetadata(default(List<string>), (d, e) =>
            {
                UpgradeWindow control = d as UpgradeWindow;

                if (control == null) return;

                List<string> config = e.NewValue as List<string>;

            }));


        public string TitleMessage
        {
            get { return (string)GetValue(TitleMessageProperty); }
            set { SetValue(TitleMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleMessageProperty =
            DependencyProperty.Register("TitleMessage", typeof(string), typeof(UpgradeWindow), new PropertyMetadata("当前更新版本:V1.0.0", (d, e) =>
            {
                UpgradeWindow control = d as UpgradeWindow;

                if (control == null) return;

                string config = e.NewValue as string;

            }));



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

            this.CloseAnimation(this);

        }


        /// <summary>
        /// 开始检查验证更新
        /// </summary>
        /// <param name="verstion"> 版本号 </param>
        /// <param name="url"> 下载地址 </param>
        /// <param name="messages"> 更新内容 </param>
        /// <returns></returns>
        public static bool BeginUpgrade(string verstion, string url, Func<string, string> convertToMessage, params string[] messages)
        {
            bool? find = Application.Current.Dispatcher.Invoke(() =>
            {
                UpgradeWindow window = new UpgradeWindow();
                window.TitleMessage = "发现新版本：" + verstion;
                window.Message = messages?.Select(l => convertToMessage(l))?.ToList();
                return window.ShowDialog();
            });


            if (find.Value != true)
                return false;

            if (UpgradeSetting.Instance.UseIEDownload)
            {
                //Process.Start("explorer.exe", url);
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });

                return find ?? false;
            }
            else
            {
                return Application.Current.Dispatcher.Invoke(() =>
                 {
                     DownLoadWindow downLoad = new DownLoadWindow();
                     downLoad.TitleMessage = "正在下载新版本：" + verstion;
                     downLoad.Message = messages?.Select(l => convertToMessage(l))?.ToList();
                     downLoad.Url = url;
                     return downLoad.ShowDialog() ?? false;
                 });

            }
        }

        /// <summary>
        /// 开始检查验证更新
        /// </summary>
        /// <param name="verstion"> 版本号 </param>
        /// <param name="url"> 下载地址 </param>
        /// <param name="messages"> 更新内容 </param>
        /// <returns></returns>
        public static bool BeginUpgrade(string verstion, string url, params string[] messages)
        {
            Func<string, string> convertToMessage = s =>
            {
                if (s == null) return null;

                string[] from = s.Split(new string[] { @"\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                return from.Aggregate((l, k) => l + Environment.NewLine + k);
            };

            return BeginUpgrade(verstion, url, convertToMessage, messages);
        }

    }
}
