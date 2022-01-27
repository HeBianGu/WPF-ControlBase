using HeBianGu.Base.WpfBase;
using System.Windows.Controls;

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
