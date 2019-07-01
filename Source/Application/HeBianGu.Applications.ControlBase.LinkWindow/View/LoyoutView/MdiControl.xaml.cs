using HeBianGu.Base.WpfBase;
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

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    /// <summary>
    /// MdiControl.xaml 的交互逻辑
    /// </summary>
    public partial class MdiControl : UserControl
    {
        public MdiControl()
        {
            InitializeComponent();
        }
    }

    public class SimpleViewModel : NotifyPropertyChanged
    {
        private bool _isSelected;

        public string Name { get; set; }

        public object SimpleContent { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                RaisePropertyChanged();
            }
        }
    }
}
