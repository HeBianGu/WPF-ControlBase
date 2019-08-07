using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// TreeListControl.xaml 的交互逻辑
    /// </summary>
    public partial class TreeListControl : UserControl
    {
        public TreeListControl()
        {
            InitializeComponent();
        }

        private void Txt_filter_TextChanged(object sender, TextChangedEventArgs e)
        { 
            ServiceRegistry.Instance.GetInstance<TreeListViewModel>().RefreshFilter(((TextBox)sender).Text);
        }
    }

}
