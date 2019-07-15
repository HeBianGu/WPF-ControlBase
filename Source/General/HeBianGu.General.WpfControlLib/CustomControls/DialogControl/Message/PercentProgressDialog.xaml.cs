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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Interaction logic for SampleMessageDialog.xaml
    /// </summary>
    public partial class PercentProgressDialog : UserControl, IPercentProgress
    {
        public PercentProgressDialog()
        {
            InitializeComponent();
        }

        public int Value
        {
            set
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.progress.Value = value;
                });
            }
        }

    }
    
    public interface IPercentProgress
    {

        int Value { set; }
    }

}
