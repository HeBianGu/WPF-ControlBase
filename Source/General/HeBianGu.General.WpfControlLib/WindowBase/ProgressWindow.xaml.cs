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
    /// ProgressWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressWindow 
    {
        public ProgressWindow()
        {
            InitializeComponent();

            this.Style = this.FindResource("MessageWindowStyle") as Style;
        }

        /// <summary> 显示窗口 </summary>
        public static void ShowDialogWith(Action<ProgressWindow> action, string title = null, params Tuple<string, Action>[] acts)
        {
            ProgressWindow p = new ProgressWindow();

            if (!string.IsNullOrEmpty(title))
            {
                p.Title = title;
            }

            if (acts == null || acts.Length == 0)
            {

            }
            else
            {
                p.actionPanel.Children.Clear();

                // Todo ：根据事件加载按钮 2017-07-28 10:46:07
                foreach (var item in acts)
                {
                    FButton f = new FButton();
                    f.Content = item.Item1;
                    //f.MinWidth = 300;
                    //f.Width = double.NaN;
                    //f.Height = 80;
                    f.Margin = new Thickness(0, 0, 20, 0);
                    //f.FIcon = "";

                    f.Style = f.FindResource("DefaultFButtonStyle") as Style;

                    f.Click += (object sender, RoutedEventArgs e) =>
                    {
                        p.BegionStoryClose();

                        if (item.Item2 != null)
                        {
                            item.Item2.Invoke();
                        }
                    };

                    p.actionPanel.Children.Add(f);
                }
            }

            Task t = new Task(() => action(p));
            t.Start();
            p.ShowDialog();
        }

        private void sumitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.BegionStoryClose();
        }

        public void SetPercent(double total, double value, string message)
        {
            Action action = () =>
            {
                this.Title = message;
                this.pb_value.Maximum = total;
                this.pb_value.Value = value;
                this.tb_percent.Text = string.Format("{0}%", Math.Round((value * 100 / total), 2).ToString());
            };

            this.Dispatcher.Invoke(action);
        }

        public override void BegionStoryClose()
        {
            CloseStoryService.Instance.UoToDownClose(this);
        }
    }
}
