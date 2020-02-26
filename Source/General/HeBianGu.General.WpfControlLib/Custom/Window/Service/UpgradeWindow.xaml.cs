using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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



        private void FButton_Click(object sender, RoutedEventArgs e)
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
            DownLoadWindow downLoad = new DownLoadWindow();
            UpgradeWindow window = new UpgradeWindow();
            window.TitleMessage = "发现新版本：" + verstion;

            window.Message = messages?.Select(l => convertToMessage(l))?.ToList();

            var find = window.ShowDialog();

            if (find.HasValue && find.Value)
            {
                downLoad.TitleMessage = "正在下载新版本：" + verstion;
                downLoad.Message = messages?.Select(l => convertToMessage(l))?.ToList();
                downLoad.Url = url;
                return downLoad.ShowDialog() ?? false;

            }

            return find ?? false;
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
                var from = s.Split(new string[] { @"\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                return from.Aggregate((l, k) => l + Environment.NewLine + k);
            };

            return BeginUpgrade(verstion, url, convertToMessage, messages);
        }

    }
}
