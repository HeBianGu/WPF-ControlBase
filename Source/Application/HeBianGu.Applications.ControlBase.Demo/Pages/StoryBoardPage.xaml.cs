using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlDemo.View
{
    /// <summary>
    /// StoryBoardPage.xaml 的交互逻辑
    /// </summary>
    public partial class StoryBoardPage : Page
    {
        public StoryBoardPage()
        {
            InitializeComponent();
        }

        private void btn_sumit_Click(object sender, RoutedEventArgs e)
        {
            List<DependencyObject> fs = new List<DependencyObject>();

            foreach (var item in this.sp_all.Children)
            {
                fs.Add(item as DependencyObject);
            }

            StoryBoardService.FountainAnimation(fs);
        }
    }
}
