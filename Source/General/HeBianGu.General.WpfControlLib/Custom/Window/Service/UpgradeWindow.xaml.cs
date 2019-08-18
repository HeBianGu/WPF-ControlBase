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
    public partial class UpgradeWindow : DialogWindow
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
    }
}
