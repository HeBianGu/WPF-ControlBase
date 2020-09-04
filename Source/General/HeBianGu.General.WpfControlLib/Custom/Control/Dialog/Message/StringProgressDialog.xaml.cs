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
    public partial class StringProgressDialog : UserControl, IStringProgress
    {
        public StringProgressDialog()
        {
            InitializeComponent();
        }

        public string MessageStr
        {
            set
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Message.Content = value; ;
                });

            }
        }

    }

    public interface IStringProgress
    {
        string MessageStr { set; }
    }
}
